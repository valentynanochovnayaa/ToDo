using System;
using Data.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataConfigurations
{
    public class UserConfiguration: IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
           builder
           .HasOne<User>()
           .WithMany(t => t.ToDoItems)
           .HasForeignKey(nameof(ToDoItem.UserId));
        }
    }
}