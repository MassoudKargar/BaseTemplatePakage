namespace BaseTemplate.Core.Contracts.People.ViewModels;

public class PersonUpdateViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class PersonUpdateValidator : AbstractValidator<PersonUpdateViewModel>
{
    public PersonUpdateValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}