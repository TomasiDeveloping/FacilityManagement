using Api.Configurations;
using Application.Interfaces;
using Application.Models;
using HealthChecks.UI.Client;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Persistence.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);
});

// Register customer services in the container
builder.Services.AddSingleton(builder.Configuration.GetSection("EmailSettings").Get<EmailConfiguration>());
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

// Register services in the container
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCors();
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureHealthChecks(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration.GetSection("JwtSettings"));
builder.Services.ConfigureSwagger();
builder.Services.ConfigureAutoMapper();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
