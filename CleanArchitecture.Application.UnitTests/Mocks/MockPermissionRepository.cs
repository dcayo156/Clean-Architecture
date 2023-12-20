using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public class MockPermissionRepository
    {
        public static void AddDataPermissionRepository(N5NowDbContext permissionDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var permissions = fixture.CreateMany<Permission>().ToList();

            permissions.Add(fixture.Build<Permission>()
               .With(tr => tr.Id, 8001)
               .With(tr => tr.PermissionTypeId, 1)
               .Create()
           );

            permissionDbContextFake.Permissions!.AddRange(permissions);
            permissionDbContextFake.SaveChanges();
        }
    }
}
