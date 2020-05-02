using LubyTasks.Domain.Commands.Entities;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LubyTasks.Domain.Commands.DbConfig
{
    public class TaskDbConfig : IEntityTypeConfiguration<Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Entities.Task> builder)
        {
            var sortLength = Convert.ToInt32(ELimitCaracteres.Sort);
            var longLength = Convert.ToInt32(ELimitCaracteres.Long);

            builder.ToTable("actions", schema: "dbo");
            builder.HasKey(t => t.Id).HasName("id");

            builder.Property(t => t.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(t => t.Title).HasColumnName("title").HasMaxLength(sortLength).HasColumnType($"nvarchar({sortLength})").IsRequired();
            builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(longLength).HasColumnType($"nvarchar({longLength})");
            builder.Property(t => t.Concluded).HasColumnName("concluded").HasColumnType("bit");
            builder.Property(t => t.UserId).HasColumnName("id_user").HasColumnType("int").IsRequired();
            builder.Property(u => u.CreateDate).HasColumnName("create_date").HasColumnType("datetimeoffset").HasDefaultValue(DateTimeOffset.Now);
            builder.Property(u => u.LastModified).HasColumnName("last_modified").HasColumnType("datetimeoffset").HasDefaultValue(DateTimeOffset.Now);
            builder.Property(t => t.Removed).HasColumnName("removed").HasColumnType("bit");

            builder.HasOne(t => t.User);
            builder.HasQueryFilter(t => !t.Removed);
        }
    }
}
