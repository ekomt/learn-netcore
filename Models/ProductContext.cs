using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models.Auth;

//if migration failed, remove --startup-project from command : dotnet ef --startup-project database update
namespace ProductApi.Models
{
    public class ProductContext : IdentityDbContext<User, Role, Guid>
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // builder
            //     .ApplyConfiguration(new MusicConfiguration());

            // builder
            //     .ApplyConfiguration(new ArtistConfiguration());
        }
    }
}
