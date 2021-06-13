using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Common;
using WebApi.Domain.Models;
using WebApi.Infra.Mapping;

namespace WebApi.Infra
{
    public class QuestionBackEndDbContext : DbContext
    {
        public QuestionBackEndDbContext(DbContextOptions<QuestionBackEndDbContext> options)
          : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new UserRoleMapping());

            modelBuilder.Entity<Role>().HasData(
        new Role
        {
            Id = 1,
            Name = "Admin",
        },
             new Role
             {
                 Id = 2,
                 Name = "Client",
             }
    );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Admin", CreatedOn = DateTime.Now, Password = "123", UserName = "Admin" }
            );

            modelBuilder.Entity<UserRole>().HasData(
          new UserRole { Id = 1, UserId = 1, RoleId = 1 }
      );

            base.OnModelCreating(modelBuilder);
        }
    }
}