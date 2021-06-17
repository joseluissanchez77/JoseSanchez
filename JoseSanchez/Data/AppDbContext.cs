using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoseSanchez.Data
{
    public class AppDbContext :  DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Restaurante> Restaurantes { get; set; }

        public DbSet<RestauranteTrx> RestaurantesTrx { get; set; }


        public DbSet<Ciudad> Ciudades { get; set; }
        

    }
}
