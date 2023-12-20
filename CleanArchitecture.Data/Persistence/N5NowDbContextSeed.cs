using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class N5NowDbContextSeed
    {
        public static async Task SeedAsync(N5NowDbContext context, ILoggerFactory loggerFactory)
        {
            if (!context.PermissionTypes!.Any())
            {
                var logger = loggerFactory.CreateLogger<N5NowDbContextSeed>();               
                context.PermissionTypes!.AddRange(GetPreconfiguredPermissionTypes());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {context}", typeof(N5NowDbContext).Name);
            }
        }
    
        private static IEnumerable<PermissionType> GetPreconfiguredPermissionTypes()
        {
            return new List<PermissionType>
            {
                new PermissionType {Description = "Solicitud de permiso"},
                new PermissionType {Description = "Permiso por vacaciones"},
                new PermissionType {Description = "Permiso por matrimonio"}
            };

        }

    }
}
