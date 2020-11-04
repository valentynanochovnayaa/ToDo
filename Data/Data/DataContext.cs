using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {}
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost; Port=5432; Database=tododb; Username=postgres; password=mimicry");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> Tasks { get; set; }
        
    }
}
