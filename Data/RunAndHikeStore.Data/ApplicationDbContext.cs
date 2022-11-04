﻿namespace RunAndHikeStore.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Models;
    using RunAndHikeStore.Data.Models;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoriesProducts { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<ProductSize> ProductsSizes { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<ShoppingCartProduct> ShoppingCartsProducts { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Size>()
                .Property("IsInteger")
                .HasDefaultValue(false);

            builder.Entity<ProductSize>()
                .Property("IsDeleted")
                .HasDefaultValue(false);

            builder.Entity<CategoryProduct>()
                .Property("IsDeleted")
                .HasDefaultValue(false);

            builder.Entity<ProductSize>()
                .HasKey(ps => new { ps.SizeId, ps.ProductId });

            builder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder.Entity<ShoppingCartProduct>()
                .HasKey(sp => new { sp.ShoppingCartId, sp.ProductId });

            builder.Entity<ApplicationUser>()
            .Property(u => u.Email)
            .HasMaxLength(EmailMaxLength)
            .IsRequired();

            builder.Entity<ApplicationUser>()
           .HasOne(u => u.ShoppingCart)
           .WithOne(u => u.ApplicationUser)
           .HasForeignKey<ShoppingCart>(s => s.ApplicationUserId);

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
