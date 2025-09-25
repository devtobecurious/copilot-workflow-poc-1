using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace TestWithCopilotVS
{
    public static class CustomCorsExtensions
    {
        public const string CorsPolicyName = "AllowAll";
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
            if (allowedOrigins == null || allowedOrigins.Length == 0)
            {
                throw new InvalidOperationException("No allowed origins configured in appsettings. Please set the 'AllowedOrigins' array in your configuration file.");
            }
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
