using System.Text;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthenticationService.Business.Security.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Configure connection String
Settings.ConnectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")!;

Settings.SecretKey = builder.Configuration["AuthenticationService_JWT_SECRET_KEY"] ?? "";

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer = "AuthenticationServiceApi",

            ValidAudience = "AuthenticationServiceApiUsers",

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Settings.SecretKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "Ownership",
        policy =>
        {
            policy.RequireAuthenticatedUser();

            policy.AddRequirements(
                new OwnershipRequirement());
        });

    options.AddPolicy(
        "Users.Create",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Users.Create"));
        });

    options.AddPolicy(
        "Users.Read",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Users.Read"));
        });

    options.AddPolicy(
        "Users.Update",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Users.Update"));
        });

    options.AddPolicy(
        "Users.Delete",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Users.Delete"));
        });

    options.AddPolicy(
        "Roles.Create",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Roles.Create"));
        });

    options.AddPolicy(
        "Roles.Read",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Roles.Read"));
        });

    options.AddPolicy(
        "Roles.Update",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Roles.Update"));
        });

    options.AddPolicy(
        "Roles.Delete",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Roles.Delete"));
        });

    options.AddPolicy(
        "Permissions.Create",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Permissions.Create"));
        });

    options.AddPolicy(
        "Permissions.Read",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Permissions.Read"));
        });

    options.AddPolicy(
        "Permissions.Update",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Permissions.Update"));
        });

    options.AddPolicy(
        "Permissions.Delete",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Permissions.Delete"));
        });

    options.AddPolicy(
        "Profile.Read",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Profile.Read"));
        });

    options.AddPolicy(
        "Profile.Update",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("Profile.Update"));
        });

    options.AddPolicy(
        "LoginHistory.Read",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("LoginHistory.Read"));
        });

    options.AddPolicy(
        "AuditLogs.Read",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement("AuditLogs.Read"));
        });
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("AuthenticationServiceLimiter", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });
});

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, OwnershipAuthorizationHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Authentication Service API",
        Version = "v2",
        Description = "Authentication & Authorization Service",
        Contact = new()
        {
            Name = "Ahmed Kelany"
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        Scheme = "Bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            new string[] { }
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(builder.Configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>()!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
