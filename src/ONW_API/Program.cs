using Microsoft.EntityFrameworkCore;
using ONW_API.Application.Transporters;
using ONW_API.Domain.Repositories;
using ONW_API.Infrastructure.Data;
using OnWay.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OnWayDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ITransporterRepository, TransporterRepository>();


builder.Services.AddScoped<CreateTransporterUseCase>();

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

app.UseHttpsRedirection();



app.MapControllers();
app.Run();


