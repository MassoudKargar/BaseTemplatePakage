using BaseTemplate.Core.ApplicationService.People.ViewModels;

namespace BaseTemplate.Core.ApplicationService.People.Validators;

public class PersonUpdateValidator : AbstractValidator<PersonUpdateViewModel>
{
    public PersonUpdateValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}