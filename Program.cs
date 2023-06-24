using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Recipes.Api.Data;

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
builder.Services.AddSwaggerGen();

// Services:
builder.Services.AddSingleton<DataContext>();

// Connection string and database service registration
var connectionString = config.GetConnectionString("DataContext");
builder.Services.AddDbContextFactory<DataContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

