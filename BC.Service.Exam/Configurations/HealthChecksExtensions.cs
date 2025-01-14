namespace BC.Service.Exam.Configurations
{
    using System.Text.Json;
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Defines the <see cref="HealthChecksExtensions" />
    /// </summary>
    public static class HealthChecksExtensions
    {
        /// <summary>
        /// The ConfigureHealthChecks
        /// </summary>
        /// <param name="builder">The builder<see cref="WebApplicationBuilder"/></param>
        /// <returns>The <see cref="WebApplicationBuilder"/></returns>
        public static WebApplicationBuilder ConfigureHealthChecks(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddMySql(
                     connectionString: builder.Configuration["Data:MysqlConnectionString"],
                      name: "MySQL Check",
                      timeout: TimeSpan.FromSeconds(5),
                      tags: ["live"]).

                AddRedis(builder.Configuration["Data:RedisConnectionString"],
                     name: "Redis Check",
                     timeout: TimeSpan.FromSeconds(5),
                     tags: ["live"])

             .AddUrlGroup(new Uri("http://localhost:8080/health"),
             name: "API Health Check",
             timeout: TimeSpan.FromSeconds(5),
             tags: ["live"]);

            builder.Services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return builder;
        }


        public static IEndpointRouteBuilder UseHealthCheck(this IEndpointRouteBuilder app)
        {
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapHealthChecks("/health");

            // Configure Health Checks Endpoints
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false, // Only indicates liveness
                ResponseWriter = WriteHealthCheckResponse
            });

            app.MapHealthChecksUI();

            return app;
        }

        // Optional: Custom response writer

        /// <summary>
        /// The WriteHealthCheckResponse
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/></param>
        /// <param name="report">The report<see cref="HealthReport"/></param>
        /// <returns>The <see cref="Task"/></returns>
        internal static Task WriteHealthCheckResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description
                }),
                totalDuration = report.TotalDuration.TotalMilliseconds
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
