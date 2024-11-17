namespace BaseTemplate.Endpoints.API.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Person, PersonInsertViewModel>().ReverseMap();
        CreateMap<Person, PersonUpdateViewModel>().ReverseMap();
        CreateMap<Person, PersonListViewModel>().ReverseMap();
        CreateMap<Person, PersonSelectViewModel>().ReverseMap();
    }
}
