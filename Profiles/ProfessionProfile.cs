using AutoMapper;
using PeopleAPI.Models;
using PeopleAPI.Dto;
using Microsoft.EntityFrameworkCore.Internal;

public class ProfessionProfile : Profile
{
    public ProfessionProfile()
    {
        CreateMap<ProfessionModel, ProfessionViewDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));
        CreateMap<ProfessionAddEditDto, ProfessionModel>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));
           // .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id));

        
    }
}