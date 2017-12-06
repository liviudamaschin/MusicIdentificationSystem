using FluentValidation;
using FluentValidation.Results;
using MusicIdentificationSystem.Backoffice.Helpers;
using MusicIdentificationSystem.Backoffice.Models;
using MusicIdentificationSystem.DAL;
using System;
using System.Linq.Expressions;

namespace MusicIdentificationSystem.Backoffice.Validators
{
    public class RegisterViewValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Numele este obligatoriu!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola este obligatorie!");
            RuleFor(x => x.Email).Length(0, 255).WithMessage("Emailul este obligatoriu!");

            Custom(x =>
            {
                if (!String.IsNullOrEmpty(x.Email))
                {
                    if (!CommonHelper.IsValidEmail(x.Email))
                        return new ValidationFailure("Email", "Emailul este incorect!");
                }
                return null;
            });

            Custom(x =>
            {
                var unitOfWork = new UnitOfWork();

                if (unitOfWork.ClientRepository.AlreadyExists(propertyOrField: "Email", value: x.Email))
                    return new ValidationFailure("Email", "Emailul deja exista in sistem");

                return null;
            });
        }
    }
}