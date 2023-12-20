using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Perminissions.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
    {
        private readonly ILogger<CreatePermissionCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContentIndexManager _contentIndexManager;

        public CreatePermissionCommandHandler(ILogger<CreatePermissionCommandHandler> logger, 
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IContentIndexManager contentIndexManager)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contentIndexManager = contentIndexManager;
        }

        public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionEntity = _mapper.Map<Permission>(request);

            _unitOfWork.Repository<Permission>().AddEntity(permissionEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("The Permission record was not inserted");
                throw new Exception("Failed to insert the Permission");
            }

            //TODO: Implement pattern Specification
            var permission = await _unitOfWork.Repository<Permission>()
                .GetAsync(x => x.Id == permissionEntity.Id,
                null,
                "PermissionType",
                true);

            if (permission.Count > 0)
            {
                var documentPermission = new DocumentIndex()
                {
                    Id = permissionEntity.Id,
                    EmployeeSurname = permission.FirstOrDefault().EmployeeSurname,
                    EmployeeForename = permission.FirstOrDefault().EmployeeForename,
                    PermissionDate = permission.FirstOrDefault().PermissionDate,
                    PermissionTypeId = permission.FirstOrDefault().PermissionTypeId,
                    Description = permission.FirstOrDefault().PermissionType.Description,
                };
                _contentIndexManager.InsertMetadata(documentPermission);
            }
            else
                _logger.LogError("The Permission record was not inserted in elastickSearch");
            return permissionEntity.Id;
        }
    }
}
