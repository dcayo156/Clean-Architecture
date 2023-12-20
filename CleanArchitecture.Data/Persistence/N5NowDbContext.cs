using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class N5NowDbContext : DbContext
    {
        public N5NowDbContext(DbContextOptions<N5NowDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                { 
                    case EntityState.Added:
                        entry.Entity.CreatedDate =  DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionType>()
                .HasMany(m => m.Permissions)
                .WithOne(m => m.PermissionType)
                .HasForeignKey(m => m.PermissionTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);            
        }

        public DbSet<Permission>? Permissions { get; set; }
        public DbSet<PermissionType>? PermissionTypes { get; set; }
    }
}
