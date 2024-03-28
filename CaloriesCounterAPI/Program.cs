using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CaloriesCounterAPI.Controllers;
using CaloriesCounterAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CaloriesCounterAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CaloriesCounterAPIContext") ?? throw new InvalidOperationException("Connection string 'CaloriesCounterAPIContext' not found.")));
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
