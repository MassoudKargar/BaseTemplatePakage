namespace BaseTemplate.Core.Contracts.People.ViewModels;

public class PersonInsertViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class PersonInsertValidator : AbstractValidator<PersonInsertViewModel>
{
    public PersonInsertValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}