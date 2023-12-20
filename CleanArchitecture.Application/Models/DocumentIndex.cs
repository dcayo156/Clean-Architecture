using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Models
{
    public class DocumentIndex
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
        public string Description { get; set; }
    }
}
