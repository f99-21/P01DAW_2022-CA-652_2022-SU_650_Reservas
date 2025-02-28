using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace P01_2022_CA_652_2022_SU_650.Models
{
    public class reservaContext : DbContext
    {

        public reservaContext(DbContextOptions<reservaContext> options) : base(options) 
        {

        }

        public DbSet<roles> roles { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<sucursales> sucursales { get; set; }
        public DbSet<espacios> espacios { get; set; }
        public DbSet<reservas> reservas { get; set; }


    }
}
