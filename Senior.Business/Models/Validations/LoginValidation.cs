using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sênior.Business.Models.Validations
{
    public class LoginValidation : AbstractValidator<LoginUser>
    {
        public LoginValidation()
        {
            RuleFor(p => p.Login)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
