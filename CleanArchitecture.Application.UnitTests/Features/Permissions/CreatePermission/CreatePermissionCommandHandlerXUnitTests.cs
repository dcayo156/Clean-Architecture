using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Perminissions.Commands.CreatePermission;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mock;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Permissions.CreatePermission
{
    public class CreatePermissionCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<ILogger<CreatePermissionCommandHandler>> _logger;
        private readonly Mock<IContentIndexManager> _contentIndexManager;
        public CreatePermissionCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _contentIndexManager = new Mock<IContentIndexManager>();

            _logger = new Mock<ILogger<CreatePermissionCommandHandler>>();


            MockPermissionRepository.AddDataPermissionRepository(_unitOfWork.Object.N5NowDbContext);
        }

        [Fact]
        public async Task CreatePermissionCommand_InputPermission_ReturnsNumber()
        {
            var PermissionInput = new CreatePermissionCommand
            {
                EmployeeForename = "Fernandez",
                EmployeeSurname = "Lopez",
                PermissionTypeId = 1,
                PermissionDate = DateTime.Now
                
            };

            var handler = new CreatePermissionCommandHandler(_logger.Object, _mapper, _unitOfWork.Object, _contentIndexManager.Object);

            var result = await handler.Handle(PermissionInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
