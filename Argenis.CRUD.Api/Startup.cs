using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NewRelic.LogEnrichers.Serilog;
using Argenis.CRUD.Api.Middlewares;
using Argenis.CRUD.Configurations;
using Argenis.CRUD.Extensions;
using Serilog;
using Serilog.Filters;
using System;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Argenis.CRUD.Api.Models;

namespace Argenis.CRUD
{
    public class Startup
    {
        private readonly IHostEnvironment Env;
        private readonly IConfiguration Configuration;
        private readonly string CorsPolicy = "_myAllowSpecificOrigins";
        private bool IsDevEnvironment => Env.IsDevelopment() || Env.IsEnvironment("Local");
        private bool IsLocalEnvironment => Env.IsEnvironment("Local");
        private bool IsPrdEnvironment => Env.IsEnvironment("Production");

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;

            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Filter.ByExcluding(Matching.WithProperty("RequestPath", "/api/healthcheck"))
                .Filter.ByExcluding(Matching.WithProperty("RequestPath", "/api/healthcheck/ping"));

            if (!IsLocalEnvironment)
                loggerConfig
                    .Enrich.FromLogContext()
                    .Enrich.WithNewRelicLogsInContext();

            Log.Logger = loggerConfig.CreateLogger();

            Log.Information("RiscosEmpresariais service started.");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationConfig = Configuration.LoadConfiguration();
            services.AddSingleton(applicationConfig);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IActionResultConverter, ActionResultConverter>();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            if (applicationConfig.CorsOrigins?.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(applicationConfig.CorsOrigins)
                        .WithHeaders(applicationConfig.CorsHeaders)
                        .AllowAnyMethod();
                    });
                });
            }

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            RepositoryConfig.ConfigureServices(services, applicationConfig);
            UseCaseConfig.ConfigureServices(services, applicationConfig);
            ValidatorConfig.ConfigureServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Argenis.CRUD",
                    Version = "v1",
                });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Argenis.CRUD.XML");
                c.IncludeXmlComments(filePath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (IsDevEnvironment)
                app.UseDeveloperExceptionPage();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });

            app.UseStaticFiles();

            if (!IsPrdEnvironment)
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api-docs/{documentName}/swagger.json";
                }).UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Argenis.CRUD API v1");
                    c.RoutePrefix = "api-docs";
                });
            }

            app.UseRouting();
            app.UseCors(CorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            Log.Information($"{Assembly.GetExecutingAssembly().GetName().Name} started");
        }
    }
}
