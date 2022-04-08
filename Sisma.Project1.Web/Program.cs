using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(item => item.AddProfile(new MyAutoMapperProfiles()));
builder.Services.AddLogging(builder0 =>
            {
                // Log4net
                builder0.SetMinimumLevel(LogLevel.Trace);
                builder0.AddLog4Net("log4net.config");
            });
builder.Services.AddDbContextFactory<SismaContext>();

//DI
builder.Services.AddSingleton<SismaContextFactory>();
builder.Services.AddScoped<SismaBL>();
builder.Services.AddScoped<SismaBL.CRUD>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
