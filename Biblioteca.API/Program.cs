using Auth.Core.Extensions;
using Auth.Core.Services;
using Biblioteca.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(options => options.UseSqlServer("Data Source=2857AL17;Initial Catalog=BibliotecaDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True"));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<TokenService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
Debug.WriteLine("Registrando TokenService na API...");
app.Run();
