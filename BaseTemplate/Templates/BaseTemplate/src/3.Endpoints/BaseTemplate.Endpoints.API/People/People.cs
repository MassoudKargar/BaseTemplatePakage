using BaseTemplate.Core.ApplicationService.People.Validators;
using BaseTemplate.Core.ApplicationService.People.ViewModels;

namespace BaseTemplate.Endpoints.API.People;

public class PeopleController(IGenericService<Person, long> personRepository, ILogger<PeopleController> logger)
    : GenericController<Person, long, PersonListViewModel, PersonUpdateViewModel, PersonUpdateValidator, PersonInsertViewModel, PersonInsertValidator, PersonSelectViewModel>(personRepository, logger);