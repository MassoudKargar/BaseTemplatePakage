namespace BaseTemplate.Core.Contracts.People.ViewModels;

public class PersonListViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}