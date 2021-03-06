using Microsoft.Extensions.DependencyInjection;
using Polly;
using Argenis.CRUD.Repositories.Base;
using System;
using System.Net;
using System.Net.Http;
using Argenis.CRUD.Borders.Repositories.Base;
using Argenis.CRUD.Repositories.Clients;
using Argenis.CRUD.Borders.Repositories.Clients;

namespace Argenis.CRUD.Configurations
{
    public static class RepositoryConfig
    {
        public static void ConfigureServices(IServiceCollection services, Shared.Configurations.ApplicationConfig applicationConfig)
        {
            services.AddSingleton<IRepositoryHelper, RepositoryHelper>();
            services.AddSingleton<IClientsRepository, ClientsRepository>();
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5));
        }
    } 
}