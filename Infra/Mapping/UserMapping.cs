using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApi.Common;
using WebApi.Domain.Models;

namespace WebApi.Infra.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);      
            entity.Property(e => e.FullName);      
            entity.Property(e => e.Password);      
            entity.Property(e => e.UserName);      
            entity.Property(e => e.CreatedOn);

            entity.HasMany(e => e.UserRoles).WithOne(e=>e.User).HasForeignKey(e => e.UserId);

        }
    }
}
