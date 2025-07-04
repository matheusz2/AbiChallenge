using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Profile for mapping between Cart entities and GetCart responses
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart operations
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<Cart, GetCartResult>();
        CreateMap<CartProduct, GetCartProductResult>();
    }
} 