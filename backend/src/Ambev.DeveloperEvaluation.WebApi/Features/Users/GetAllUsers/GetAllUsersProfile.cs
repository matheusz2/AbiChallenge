using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;

/// <summary>
/// Profile for mapping GetAllUsers requests
/// </summary>
public class GetAllUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllUsers
    /// </summary>
    public GetAllUsersProfile()
    {
        CreateMap<GetAllUsersRequest, GetAllUsersCommand>()
            .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src._page))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src._size))
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src._order))
            .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => new UserFilter
            {
                Username = src.username,
                Email = src.email,
                Role = src.role,
                Status = src.status,
                FirstName = src.firstName,
                LastName = src.lastName,
                City = src.city,
                Street = src.street,
                Phone = src.phone
            }));
    }
} 