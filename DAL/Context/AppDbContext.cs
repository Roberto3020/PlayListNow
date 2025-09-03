using Microsoft.EntityFrameworkCore;
using TRANSVERSAL.Models;

namespace DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Song> Songs { get; set; }
    }
}