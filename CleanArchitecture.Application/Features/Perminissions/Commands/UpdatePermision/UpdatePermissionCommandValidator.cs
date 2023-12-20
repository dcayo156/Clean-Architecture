using FluentValidation;

namespace CleanArchitecture.Application.Features.Perminissions.Commands.UpdatePermision
{
    internal class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(p => p.EmployeeForename)
                .NotNull().WithMessage("{EmployeeForename} is not null");

            RuleFor(p => p.EmployeeSurname)
                .NotNull().WithMessage("{EmployeeSurname} is not null");

            RuleFor(p => p.PermissionTypeId)
                .NotNull().WithMessage("{PermissionType} is not null");
            
            RuleFor(p => p.PermissionDate)
                .NotNull().WithMessage("{PermissionDate} is not null");
        }
    }
}
