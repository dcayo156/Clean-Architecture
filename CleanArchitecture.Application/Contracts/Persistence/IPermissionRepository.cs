using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IAsyncRepository<Permission>
    {
    }
}
