using Dapper;
using System.Data.SqlClient;
using Minimal_API_Demo_MAD.Models;
using Minimal_API_Demo_MAD.EndPoints;
using Minimal_API_Demo_MAD.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceProvider =>
{
   var configuration = serviceProvider.GetRequiredService <IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection")??
                            throw new ApplicationException("no anduvo el connection String");
    return new SqlConnectionFactory(connectionString);
});

var app=builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapNomencladorEndPoints();

app.Run();