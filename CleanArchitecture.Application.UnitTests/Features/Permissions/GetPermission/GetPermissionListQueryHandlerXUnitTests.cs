using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Perminissions.Queries.GetPermissionList;
using CleanArchitecture.Application.Models;
using Moq;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Permissions.GetPermission
{
    public class GetPermissionListQueryHandlerXUnitTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IContentIndexManager> _contentIndexManagerMock;
        private GetPermissionQueryHandler _handler;

        public GetPermissionListQueryHandlerXUnitTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _contentIndexManagerMock = new Mock<IContentIndexManager>();
            _handler = new GetPermissionQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object, _contentIndexManagerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedPermissions()
        {
            // Arrange
            List<DocumentIndex> expectedResult = new List<DocumentIndex>
                {
                    new DocumentIndex { Id = 1, EmployeeForename = "Flores" , EmployeeSurname= "Cespedes",PermissionDate = DateTime.Now, Description = "Solicitud por vacaciones", PermissionTypeId =1},
                    new DocumentIndex { Id = 2, EmployeeForename = "Pereira" ,EmployeeSurname = "Cespedes",PermissionDate = DateTime.Now, Description ="Solicitud de permiso", PermissionTypeId =2}
                };
            var permissions = new List<PermissionVM>();
            //var searchResult = new List<Metadata>();
            _contentIndexManagerMock.Setup(x => x.SearchMetadata("*")).Returns(expectedResult);
            _mapperMock.Setup(x => x.Map<List<PermissionVM>>(expectedResult)).Returns(permissions);

            // Act
            var result = await _handler.Handle(new GetPermissionQuery(), new CancellationToken());

            // Assert
            Assert.Equal(permissions, result);
        }
    }
}
