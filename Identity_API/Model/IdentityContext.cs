using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity_API;
using Identity_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity_Service.Model
{
    public class IdentityContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }    

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfig.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.id);
            modelBuilder.Entity<User>()
                .Property(u => u.id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.surname)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.password)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
               .HasMany(r => r.userRoles)
               .WithOne(ur => ur.User)
               .HasForeignKey(ur => ur.UserId)
               .HasConstraintName("UserRole_User_Fk");

            modelBuilder.Entity<Request>()
                .HasKey(r => r.id);
            modelBuilder.Entity<Request>()
                .Property(r => r.id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Request_Id");
            modelBuilder.Entity<Request>()
               .Property(r => r.creationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Creation_Time");
            modelBuilder.Entity<Request>()
                .Property(r => r.text)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Request>()
                .Property(r => r.userId)
                .IsRequired()
                .HasColumnName("User_Id");
            modelBuilder.Entity<Request>()
                .HasOne(r => r.user)
                .WithMany(u => u.requests)
                .HasForeignKey(r => r.userId)
                .HasConstraintName("Request_User_Fk");

            modelBuilder.Entity<Role>()
                .HasKey(r => r.id);
            modelBuilder.Entity<Role>()
                .Property(r => r.id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Role_Id");
            modelBuilder.Entity<Role>()
                .Property(r => r.name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Role>()
                .Property(r => r.description)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.userRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .HasConstraintName("UserRole_Role_Fk");

            //modelBuilder.Entity<UserRole>()
            //    .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.UserId)
                .HasColumnName("User_Id");
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.RoleId)
                .HasColumnName("Role_Id");
            modelBuilder.Entity<UserRole>()
               .HasKey(ur => ur.Id);
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.Id)
                .HasColumnName("User_Role_Id")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.StartDate)
                .HasDefaultValueSql("now()")
                //.HasColumnType("timestamp without time zone")
                .HasColumnName("Start_Time");
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.EndDate)
                //.HasColumnType("timestamp without time zone")
                .HasColumnName("End_Time");
        }
    }
}
