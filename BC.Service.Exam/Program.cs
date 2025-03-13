using System.IO.Compression;
using BC.Service.Exam.Configurations;
using BC.Service.Exam.Exceptions;
using Microsoft.AspNetCore.ResponseCompression;

namespace BC.Service.Exam;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureLogger();

        builder.Services.AddOptions();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddProblemDetails();

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        builder.Services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
        });

        builder.ConfigureHeaderPropagation();

        builder.Services.AddHttpContextAccessor()
            .AddResponseCompression();

        // Configure Swagger
        builder.AddSwaggerConfiguration();

        // Configure Database
        // builder.AddDataBase();

        builder.ConfigureServices();

        builder.ConfigureHealthChecks();

        builder.AddCustomCors();

        var app = builder.Build();

        app.MapControllers();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            // app.MigrateDatabase();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseResponseCompression();
        }

        app.UseSwaggerSetup();

        app.UseHealthCheck();

        app.Run();
    }
}