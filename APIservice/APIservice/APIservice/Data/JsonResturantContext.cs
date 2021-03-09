using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIservice.Models;
using Microsoft.EntityFrameworkCore;

namespace APIservice.Services
{
    public class JsonResturantContext : DbContext
    {
        public JsonResturantContext(DbContextOptions<JsonResturantContext> options)
            : base(options){}

        public DbSet<Resturant> Resturants { get; set; }
    }
}
