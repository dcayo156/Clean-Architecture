using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Perminissions.Commands.UpdatePermision
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePermissionCommandHandler> _logger;
        private readonly IContentIndexManager _contentIndexManager;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdatePermissionCommandHandler> logger,
            IContentIndexManager contentIndexManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _contentIndexManager = contentIndexManager;
        }

        public async Task<Unit> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToUpdate = await _unitOfWork.PermissionRepository.GetByIdAsync(request.Id);

            if (permissionToUpdate == null)
            {
                _logger.LogError($"Permission id {request.Id} not found");
                throw new NotFoundException(nameof(Permission), request.Id);
            }

            _mapper.Map(request, permissionToUpdate, typeof(UpdatePermissionCommand), typeof(Permission));

            _unitOfWork.PermissionRepository.UpdateEntity(permissionToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"Operation was successful updating Permission {request.Id}");


            //TODO: Implement pattern Specification
            var permission = await _unitOfWork.Repository<Permission>()
                 .GetAsync(x => x.Id == permissionToUpdate.Id,
                 null,
                 "PermissionType",
                 true);
            if (permission.Count > 0)
            {
                var documentPermission = new DocumentIndex()
                {
                    Id = permissionToUpdate.Id,
                    EmployeeSurname = permission.FirstOrDefault().EmployeeSurname,
                    EmployeeForename = permission.FirstOrDefault().EmployeeForename,
                    PermissionDate = permission.FirstOrDefault().PermissionDate,
                    PermissionTypeId = permission.FirstOrDefault().PermissionTypeId,
                    Description = permission.FirstOrDefault().PermissionType.Description,
                };
                _contentIndexManager.UpdateMetadata(documentPermission);
            }
            else
                _logger.LogError("The Permission record was not updating in elastickSearch");

            return Unit.Value;
        }
    }
}
