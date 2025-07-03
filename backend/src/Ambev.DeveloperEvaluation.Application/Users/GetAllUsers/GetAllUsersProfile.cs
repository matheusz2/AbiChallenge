using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Profile for mapping between User entity and GetAllUsers response
/// </summary>
public class GetAllUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllUsers operation
    /// </summary>
    public GetAllUsersProfile()
    {
        CreateMap<User, GetAllUsersItemResult>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        CreateMap<Name, UserNameResult>();
        CreateMap<Address, UserAddressResult>();
        CreateMap<Geolocation, UserGeolocationResult>();
    }
} 