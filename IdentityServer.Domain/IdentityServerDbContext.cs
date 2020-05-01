using IdentityServer.Domain.Commands.DbConfig;
using IdentyServer.Domain.Commands.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Domain
{
    public class IdentityServerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfig());
        }
    }
}
