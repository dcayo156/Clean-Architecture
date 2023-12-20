using MediatR;

namespace CleanArchitecture.Application.Features.Perminissions.Queries.GetPermissionList
{
    public class GetPermissionQuery : IRequest<List<PermissionVM>>
    {
    }
}
