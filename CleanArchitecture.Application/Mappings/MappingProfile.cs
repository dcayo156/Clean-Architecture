using AutoMapper;
using CleanArchitecture.Application.Features.Perminissions.Commands.CreatePermission;
using CleanArchitecture.Application.Features.Perminissions.Commands.UpdatePermision;
using CleanArchitecture.Application.Features.Perminissions.Queries.GetPermissionList;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePermissionCommand, Permission>();
            CreateMap<UpdatePermissionCommand, Permission>();
            CreateMap<Permission, PermissionVM>();
            CreateMap<DocumentIndex, PermissionVM>();
        }
    }
}
