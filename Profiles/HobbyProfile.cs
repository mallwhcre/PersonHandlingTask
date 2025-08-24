using AutoMapper;
using PeopleAPI.Models;
using PeopleAPI.Dto;
using Microsoft.EntityFrameworkCore.Internal;

public class HobbyProfile : Profile
{
    public HobbyProfile()
    {
        CreateMap<HobbyModel, HobbyViewDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));
        CreateMap<HobbyViewDto, HobbyModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id));
    }
}