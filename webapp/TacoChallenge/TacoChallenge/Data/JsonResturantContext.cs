using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TacoChallenge.Data
{
    public class JsonResturantContext : DbContext
    {
        public JsonResturantContext(DbContextOptions<JsonResturantContext> options)
            : base(options){}

        public DbSet<TacoChallenge.Models.Resturant> Resturants { get; set; }
    }
}
