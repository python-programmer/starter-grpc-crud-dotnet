using AutoMapper;
using ProtobufCRUDServer;
using ProtobufCRUDServer.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePersonRequest, PersonEntity>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateTime.Parse(src.Birthday)));

        CreateMap<PersonEntity, CreatePersonResponse>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToShortDateString()));

        CreateMap<PersonEntity, GetPersonResponse>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToShortDateString()));

        CreateMap<UpdatePersonRequest, PersonEntity>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => DateTime.Parse(src.Birthday)))
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PersonEntity, UpdatePersonResponse>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToShortDateString()));
        
        CreateMap<PersonEntity, DeletePersonResponse>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToShortDateString()));
    }
}