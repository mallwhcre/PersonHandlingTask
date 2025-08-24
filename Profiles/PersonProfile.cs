using AutoMapper;
using PeopleAPI.Models;
using PeopleAPI.Dto;
using Microsoft.EntityFrameworkCore.Internal;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonModel, PersonViewDto>()
            .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FirstName + " " + x.LastName))
            .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.DateOfBirth))
            .ForMember(dest => dest.Comments, src => src.MapFrom(x => x.Comments))
            .ForMember(dest => dest.ProfessionName, src => src.MapFrom
            (
                x => x.Profession != null ? x.Profession.Name : null
            ))
            .ForMember(dest => dest.Hobbies, src => src.MapFrom(x => x.Hobbies));
    }
}