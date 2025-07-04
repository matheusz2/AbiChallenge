using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping CreateUser requests and responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>()
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Address.Geolocation.Long));

        CreateMap<CreateUserResult, CreateUserResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name.Firstname} {src.Name.Lastname}".Trim()));

        CreateMap<CreateUserNameResult, CreateUserNameResponse>();
        CreateMap<CreateUserAddressResult, CreateUserAddressResponse>();
        CreateMap<CreateUserGeolocationResult, CreateUserGeolocationResponse>();
    }
}
