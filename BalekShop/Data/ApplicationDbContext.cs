using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BalekShop.Data;
using System.Reflection.Emit;

namespace BalekShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<CartModel> CartModels { get; set; }
        public DbSet<CategoryModel> CategoryModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);
        }
    }
}