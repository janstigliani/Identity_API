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
        }
    }
}
