using LubyTasks.Domain.Commands.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LubyTasks.Domain.Commands.DbConfig
{
    public class UserDbConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", schema: "dbo");
            builder.HasKey(u => u.Id).HasName("id");
            builder.HasIndex(u => u.Login).IsUnique();

            builder.Property(u => u.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(150).HasColumnType("nvarchar(150)").IsRequired();
            builder.Property(u => u.Login).HasColumnName("login").HasMaxLength(512).HasColumnType("nvarchar(512)").IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").HasMaxLength(512).HasColumnType("nvarchar(512)").IsRequired();
            builder.Property(u => u.CreateDate).HasColumnName("create_date").HasColumnType("datetimeoffset").HasDefaultValue(DateTimeOffset.Now);
            builder.Property(u => u.LastModified).HasColumnName("last_modified").HasColumnType("datetimeoffset").HasDefaultValue(DateTimeOffset.Now);
            builder.Property(u => u.Removed).HasColumnName("removed").HasColumnType("bit");
            builder.HasQueryFilter(u => !u.Removed);
        }
    }
}
