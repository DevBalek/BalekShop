using System;
using Microsoft.EntityFrameworkCore;

namespace ParmisAuth.Data
{
	public class ParmisDbContext : DbContext
	{
		public ParmisDbContext()
		{
		}

		public ParmisDbContext(DbContextOptions<ParmisDbContext> options) : base(options)
		{

		}

		public DbSet<ProductModel> ProductModels { get; set; }
		public DbSet<CartModel> CartModels{ get; set; }
		public DbSet<CategoryModel> CategoryModels{ get; set; }
		public DbSet<UserModel> UserModels{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql("Data Source=localhost;Port=5432;Initial Catalog=parmisweb;User ID=postgres;Password=postgres");
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			/*
			 * 
			modelBuilder.Entity<UserModel>(entity =>
			{
				entity.Property(i => i.Id).UseIdentityColumn().IsRequired();
			});
			*/
			modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }
    }
}

