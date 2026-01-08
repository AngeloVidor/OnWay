using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ONW_API.Application.Auth;
using ONW_API.Application.Drivers;
using ONW_API.Application.Shipment;
using ONW_API.Application.Shipments;
using ONW_API.Application.Tokens;
using ONW_API.Application.Transporters;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Auth;
using ONW_API.Infrastructure.Data;
using ONW_API.Infrastructure.Repositories;
using ONW_API.Infrastructure.Security;
using ONW_API.Infrastructure.SMTP;
using OnWay.Application.Email;
using OnWay.Application.VerifyAccount;
using OnWay.Infrastructure.Repositories;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API V1", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the JWT token in the format: Bearer {token}"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddDbContext<OnWayDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<ITransporterRepository, TransporterRepository>();
builder.Services.AddScoped<ITransporterVerificationRepository, TransporterVerificationRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();

builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<CreateDriverUseCase>();
builder.Services.AddScoped<VerifyAccountUseCase>();
builder.Services.AddScoped<CreateTransporterUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<CreateShipmentUseCase>();
builder.Services.AddScoped<UpdateShipmentStatusUseCase>();
builder.Services.AddScoped<GetShipmentsByStatusUseCase>();
builder.Services.AddScoped<GetRecentShipmentsUseCase>();
builder.Services.AddScoped<CreateShipmentUseCase>();
builder.Services.AddScoped<TrackShipmentUseCase>();
builder.Services.AddScoped<GetTransporterByIdUseCase>();



builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = Environment.GetEnvironmentVariable("JWT_KEY")!;
    options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
    options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
    options.ExpirationMinutes = int.Parse(
        Environment.GetEnvironmentVariable("JWT_DURATION_IN_MINUTES")!
    );
});

builder.Services.AddSingleton<JwtSettings>();

var smtpSettings = new SmtpSettings
{
    Host = Environment.GetEnvironmentVariable("SMTP_HOST")!,
    Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!),
    Username = Environment.GetEnvironmentVariable("SMTP_USER")!,
    Password = Environment.GetEnvironmentVariable("SMTP_PASS")!,
    From = Environment.GetEnvironmentVariable("SMTP_FROM")!,
    EnableSsl = true
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("JWT_KEY")!
            )
        )
    };
});

builder.Services.AddSingleton(smtpSettings);


builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:8080") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseCors();
//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

