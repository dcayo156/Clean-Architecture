﻿using MediatR;

namespace CleanArchitecture.Application.Features.Perminissions.Commands.CreatePermission
{
    public class CreatePermissionCommand : IRequest<int>
    {
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
