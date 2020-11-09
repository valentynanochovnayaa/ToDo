using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class DataContext: IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions options):base(options)
        {}
        public DataContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost; Port=5432; Database=tododb; Username=postgres; password=mimicry");
        }
        public override DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ToDoItem>()
                .HasOne(u => u.User)
                .WithMany(t => t.ToDoItems)
                .HasForeignKey(t => t.UserId);
        }
        

    }
}
