namespace BaseTemplate.Core.Contracts.People.ViewModels;

public class PersonSelectViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public long StoreId { get; set; }
}