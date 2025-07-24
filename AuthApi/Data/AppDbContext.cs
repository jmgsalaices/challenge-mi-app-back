using Microsoft.EntityFrameworkCore;
using AuthApi.Models;
using AuthApi.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Data
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Nombre real en PostgreSQL

                entity.HasKey(e => e.Id); // Clave primaria explícita
                entity.Property(e => e.Id)
                      .HasColumnName("id")       // nombre exacto de la columna
                      .ValueGeneratedOnAdd();   // si usas SERIAL

                entity.Property(e => e.Username)
                      .HasColumnName("username")
                      .IsRequired();

                entity.Property(e => e.PasswordHash)
                      .HasColumnName("passwordhash")
                      .IsRequired();
            });
        }
    }
}
