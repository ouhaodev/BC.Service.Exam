using BC.Service.Exam.Common;
using BC.Service.Exam.DataAccess;
using BC.Service.Exam.Infrastructure;
using BC.Service.Exam.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BC.Service.Exam.Configurations
{
    public static class ServicesExtensions
    {
        public static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(builder.Configuration)
           .CreateLogger();

            builder.Host.UseSerilog();
            return builder;
        }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            return builder;
        }

        public static WebApplicationBuilder AddDataBase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySQL(builder.Configuration["Data:MysqlConnectionString"], action =>
            {
                action.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            }));
            return builder;
        }

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
            return app;
        }

        /// <summary>
        /// Configure CORS Policy
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddCustomCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(Constants.DefaultCorsPolicy,
                    builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            });

            return builder;
        }

        /// <summary>
        /// Configure HeaderPropagation for the application
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureHeaderPropagation(this WebApplicationBuilder builder)
        {
            builder.Services.AddHeaderPropagation(c =>
            {
                c.Headers.Add(Constants.AuthorizationKey);
                c.Headers.Add(Constants.CorrelationKey);
            });
        }
    }
}
