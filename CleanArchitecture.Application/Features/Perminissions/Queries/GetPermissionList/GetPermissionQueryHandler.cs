using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.Perminissions.Queries.GetPermissionList
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, List<PermissionVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IContentIndexManager _contentIndexManager;
        public GetPermissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IContentIndexManager contentIndexManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contentIndexManager = contentIndexManager;
        }

        public async Task<List<PermissionVM>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            //var permissionList = await _unitOfWork.PermissionRepository.GetAllAsync();
            var searchResult = _contentIndexManager.SearchMetadata("*");
            return _mapper.Map<List<PermissionVM>>(searchResult);
           //return _mapper.Map<List<PermissionVM>>(permissionList);
        }
    }
}
    
