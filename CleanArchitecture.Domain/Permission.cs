using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Permission : BaseDomainModel
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
