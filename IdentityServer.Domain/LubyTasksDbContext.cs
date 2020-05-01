using LubyTasks.Domain.Commands.Auth.Entities;
using LubyTasks.Domain.Commands.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace LubyTasks.Domain
{
    public class LubyTasksDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public LubyTasksDbContext(DbContextOptions<LubyTasksDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfig());
        }
    }
}
