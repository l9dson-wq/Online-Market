using Microsoft.EntityFrameworkCore;
using StockApp.Core.Domain.Common;
using StockApp.Core.Domain.Entities;

namespace StockApp.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users  { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            #region tables
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Product>().HasKey(product => product.Id);//lambda
            modelBuilder.Entity<Category>().HasKey(category => category.Id);
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Category>()
                .HasMany<Product>(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Product>(user => user.Products)
                .WithOne(product => product.User)
                .HasForeignKey(product => product.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "property configurations"

            #region products
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.Price)
              .IsRequired();
            #endregion

            #region categories

            modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            #endregion

            #region Users
            modelBuilder.Entity<User>().
                Property(user => user.password)
                .IsRequired();

            modelBuilder.Entity<User>().
                Property(user => user.Username)
                .IsRequired();

            modelBuilder.Entity<User>().
                Property(user => user.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<User>().
                Property(user => user.Email)
                .IsRequired();

            modelBuilder.Entity<User>().
                Property(user => user.Phone)
                .IsRequired();
            #endregion

            #endregion
        }


    }
}
