using MediatR;

namespace CleanArchitecture.Application.Features.Perminissions.Commands.UpdatePermision
{
    public class UpdatePermissionCommand :IRequest
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
