using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Recipes.Api.Data;
using Recipes.Api.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure the appsettings.json file
// (set the files to "copy: always")
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at:
// https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger basic documentation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Recipes API",
        Description =
            "This project aims to test a Recipes Web API using the xUnit testing framework. " +
            "The API is designed to provide various functionalities related to recipes",
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// My services:
builder.Services.AddScoped<IRecipeService, RecipeService>();

// Connection string and database service registration
var connectionString = config.GetConnectionString("DataContext");
builder.Services.AddDbContextFactory<DataContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
    
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

