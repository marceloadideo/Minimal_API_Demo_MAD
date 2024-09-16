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
            /*
            ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, new SqlParameter[] {
                                    new SqlParameter("@idTa_Vaccine", Guid.NewGuid()) ,
                                    new SqlParameter("@Code", obj.Code) ,
                                    new SqlParameter("@Description", obj.Description) ,
                                    new SqlParameter("@idTy_Immunization", obj.Immunization.idTa_Combo) ,
                                    new SqlParameter("@AgeMonth", obj.AgeMonth) ,
                                    new SqlParameter("@Notes", obj.Notes)



            */

            const string sql = "declare @descripcion varchar(40);INSERT INTO Nomencladores (codigo,descripcion)  VALUES  ('222222',@descripcion)";
            var nomencladores = await connection.QueryAsync<Nomencladores>(sql);
            return Results.Ok(nomencladores);


        });
    }
}
