using Microsoft.EntityFrameworkCore;
using Task2.Models;
using Task2.Controllers;
using Pomelo;
    
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEntityFrameworkMySql().AddDbContext <NorthwindContext> (options => {
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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