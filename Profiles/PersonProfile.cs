using AutoMapper;
using PeopleAPI.Models;
using PeopleAPI.Dto;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
            //Only Map hobbyName
            .ForMember(dest => dest.Hobbies, src => src.MapFrom(x =>
                x.Hobbies.Select(h => new HobbyViewDto { Name = h.Name }).ToList()));


        CreateMap<PersonAddEditDto, PersonModel>()
            .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
            .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
            .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.DateOfBirth))
            .ForMember(dest => dest.Comments, src => src.MapFrom(x => x.Comments))
            .ForMember(dest => dest.CreatedAt, src => src.MapFrom(x => DateTime.Now));
    }
}