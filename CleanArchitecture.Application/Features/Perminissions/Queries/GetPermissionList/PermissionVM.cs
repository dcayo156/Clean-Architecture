namespace CleanArchitecture.Application.Features.Perminissions.Queries.GetPermissionList
{
    public class PermissionVM
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
