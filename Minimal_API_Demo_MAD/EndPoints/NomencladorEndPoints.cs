using Dapper;
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
    }
}
