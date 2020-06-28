using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseRegistrationAPI.Model
{
    public class RegistrationModel : BaseModel
    {
        public RegistrationModel() 
        {
            License = "";
        }
        protected override IValidator _validator => new RegistrationModelValidator();
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string LicenseKey { get; set; }
        public string License { get; set; }
    }
    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.ContactPerson).NotEmpty();
            RuleFor(x => x.LicenseKey).NotEmpty();
            RuleFor(x => x.LicenseKey).MinimumLength(10);
            RuleFor(x => x.License).Must(x => x != "0").WithMessage("License Key not found.");
            RuleFor(x => x.License).Must(x => x != "-1").WithMessage("License Key is already used.");
            RuleFor(x => x.License).Must(x => x != null).WithMessage("License could not be generated. Please try again later.");
        }
    }
}
