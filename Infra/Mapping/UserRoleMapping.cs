using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Models;

namespace WebApi.Infra.Mapping
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id);
            entity.HasOne(e => e.Role).WithMany(e=>e.UserRoles).HasForeignKey(e=>e.RoleId);
            entity.HasOne(e => e.User).WithMany(e=>e.UserRoles).HasForeignKey(e=>e.UserId);

            
        }
    }
}
