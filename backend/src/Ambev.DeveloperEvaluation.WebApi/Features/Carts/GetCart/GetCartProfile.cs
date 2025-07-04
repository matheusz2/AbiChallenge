using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Profile for mapping GetCart requests and responses
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<GetCartResult, GetCartResponse>();
        CreateMap<GetCartProductResult, GetCartProductResponse>();
    }
} 