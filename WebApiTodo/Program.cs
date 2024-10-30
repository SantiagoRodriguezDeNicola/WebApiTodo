using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiTodo.Data;
using WebApiTodo.Controllers;
using WebApiTodo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApiTodo.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApiTodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiTodoContext") ?? throw new InvalidOperationException("Connection string 'WebApiTodoContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=Defaultconnection"));
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("swagger/v1/swagger.json", "Ejemplo de API"));

app.MapControllers();

app.MapToDoEndpoints();

app.Run();
