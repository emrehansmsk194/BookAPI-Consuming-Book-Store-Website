using BookAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  
            : base(options)
        {
            
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Book> Books { get; set; }  
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher
                {
                    Id = 1,
                    Name = "Can Yayınları",
                    Address = "Sariyer, Istanbul",
                    Website = "www.canyayinlari.com",
                    ContactNumber = "0212-252-59-88",
                    EmailAddress = "yayinevi@canyayinlari.com",
                    YearEstablished = 1981

                }
                );
        }
    }
}
