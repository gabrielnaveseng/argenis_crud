using System;

namespace Argenis.CRUD.Shared.Configurations
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            Logging = new Logging();
            Authentication = new AuthenticationConfig();
            ConnectionStrings = new ConnectionStrings();
            Product = new ApiConfig();
            Bff = new ApiConfig();
        }

        public Logging Logging { get; set; }
        public string[] CorsOrigins { get; set; } = default!;
        public string[]? CorsHeaders { get; set; }
        public AuthenticationConfig Authentication { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public ApiConfig Product { get; set; }
        public ApiConfig Bff { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasicCoverageId { get; set; }
        public Guid TheftCoverageId { get; set; }
    }

    public class Logging
    {
        public string TokenLogentries { get; set; } = default!;
    }

    public class AuthenticationConfig
    {
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
        public string Scope { get; set; } = default!;
        public string Authority { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }

    public class ApiConfig
    {
        public string BaseUrl { get; set; } = default!;
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = default!;
        public string NoSqlConnection { get; set; } = default!;
    }
}
