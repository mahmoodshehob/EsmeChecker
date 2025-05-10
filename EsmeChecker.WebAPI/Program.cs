using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.BusinessRules.Services;
using EsmeChecker.DataAccess.Data;

using EsmeChecker.DataAccess.Repository;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.StaticData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<PostgreServerDbContext>(options =>
	options.UseNpgsql(
		builder.Configuration.GetConnectionString("PostgresConnection"),
		npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", SD.Schema.EsmeCheckers)
	));


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme.",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfServices, UnitOfServices>();



//builder.Services.AddSingleton<CustomRateLimiter>();
//builder.Services.AddScoped<RateLimiterAttribute>();



var app = builder.Build();


// Configure the HTTP request pipeline.
bool enableSwagger = app.Environment.IsDevelopment() ||
					 builder.Configuration.GetValue<bool>("EnableSwaggerInProduction");

if (enableSwagger)
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
