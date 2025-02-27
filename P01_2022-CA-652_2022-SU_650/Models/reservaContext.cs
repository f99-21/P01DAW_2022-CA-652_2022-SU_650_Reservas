using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class reservaContext : DbContext
    {

        public reservaContext(DbContextOptions<reservaContext> options) : base(options) 
        {

        }

        public DbSet<rol> roles { get; set; }
        public DbSet<usuario> usuarios { get; set; }
        public DbSet<sucursal> sucursales { get; set; }
        public DbSet<espacio> espacios { get; set; }
        public DbSet<reserva> reservas { get; set; }


    }
}
