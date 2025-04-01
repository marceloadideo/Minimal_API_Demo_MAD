
using Minimal_API_Demo_MAD.Models;
using System.Collections.Generic;
namespace Minimal_API_Demo_MAD.DAL
{
    public class NomencladorContext : DbContext
    {
        public NomencladorContext(DbContextOptions<NomencladorContext> options) : base(options) { }

        public DbSet<Nomenclador> Nomencladores { get; set; }
    }
    
}
