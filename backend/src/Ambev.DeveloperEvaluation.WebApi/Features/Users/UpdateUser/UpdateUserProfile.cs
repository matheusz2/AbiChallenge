using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Profile for mapping UpdateUser requests and responses
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserCommand>()
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Address.Geolocation.Long));

        CreateMap<UpdateUserResult, UpdateUserResponse>();
        CreateMap<UpdateUserNameResult, UpdateUserNameResponse>();
        CreateMap<UpdateUserAddressResult, UpdateUserAddressResponse>();
        CreateMap<UpdateUserGeolocationResult, UpdateUserGeolocationResponse>();
    }
} 