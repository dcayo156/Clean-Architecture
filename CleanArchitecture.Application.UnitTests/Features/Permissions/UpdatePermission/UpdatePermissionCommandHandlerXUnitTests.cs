using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Perminissions.Commands.UpdatePermision;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Permissions.UpdatePermission
{
    public class UpdatePermissionCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<UpdatePermissionCommandHandler>> _logger;
        private readonly Mock<IContentIndexManager> _contentIndexManager;
        public UpdatePermissionCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _contentIndexManager = new Mock<IContentIndexManager>();

            _logger = new Mock<ILogger<UpdatePermissionCommandHandler>>();


            MockPermissionRepository.AddDataPermissionRepository(_unitOfWork.Object.N5NowDbContext);
        }

        [Fact]
        public async Task CreatePermissionCommand_InputPermission_ReturnsNumber()
        {
            var PermissionInput = new UpdatePermissionCommand
            {
                Id = 8001,
                EmployeeForename = "Fernandez",
                EmployeeSurname = "Lopez",
                PermissionTypeId = 1,
                PermissionDate = DateTime.Now
            };

            var handler = new UpdatePermissionCommandHandler(_unitOfWork.Object, _mapper, _logger.Object, _contentIndexManager.Object);

            var result = await handler.Handle(PermissionInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
