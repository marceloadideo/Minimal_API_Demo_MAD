using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Minimal_API_Demo_MAD.Models;
using System.Data.SqlClient;

namespace Minimal_API_Demo_MAD.EndPoints;

public static class NomencladorEndPoints
{
    public static void MapNomencladorEndPoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("nomencladores", async (IConfiguration configuration) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")!;
            using var connection = new SqlConnection(connectionString);
            const string sql = "Select * from Nomencladores";
            var nomencladores = await connection.QueryAsync<Nomencladores>(sql);
            return Results.Ok(nomencladores);


        });

        builder.MapPost("nomencladores", async (IConfiguration configuration) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")!;
            using var connection = new SqlConnection(connectionString);
            const string sql = "declare @descripcion varchar(40);INSERT INTO Nomencladores (codigo,descripcion)  VALUES  ('222222',@descripcion)";
            var nomencladores = await connection.QueryAsync<Nomencladores>(sql);
            return Results.Ok(nomencladores);


        });

        builder.MapGet("/nomencladores", async (IConfiguration configuration) =>
        await configuration.Nomencladores.ToListAsync());

        builder.MapGet("/nomencladores/{id}", async (int id, NomencladorContext db) =>
            await db.Nomencladores.FindAsync(id)
                is Nomencladores nomenclador
                    ? Results.Ok(nomenclador)
                    : Results.NotFound());

        builder.MapPost("/nomencladores", async (Nomenclador nomenclador, NomencladorContext db) =>
        {
            db.Nomencladores.Add(nomenclador);
            await db.SaveChangesAsync();
            return Results.Created($"/nomencladores/{nomenclador.IdNomenclador}", nomenclador);
        });

        builder.MapPut("/nomencladores/{id}", async (int id, Nomenclador inputNomenclador, NomencladorContext db) =>
        {
            var nomenclador = await db.Nomencladores.FindAsync(id);
            if (nomenclador is null) return Results.NotFound();

            nomenclador.Codigo = inputNomenclador.Codigo;
            nomenclador.Descripcion = inputNomenclador.Descripcion;
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        builder.MapDelete("/nomencladores/{id}", async (int id, NomencladorContext db) =>
        {
            var nomenclador = await db.Nomencladores.FindAsync(id);
            if (nomenclador is null) return Results.NotFound();

            db.Nomencladores.Remove(nomenclador);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

    }
}
