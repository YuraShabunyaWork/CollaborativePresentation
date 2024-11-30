using Microsoft.EntityFrameworkCore;
using PresentationApp.Models;

namespace PresentationApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
    }
}
