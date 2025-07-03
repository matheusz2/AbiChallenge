using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Profile for mapping between Cart entities and GetAllCarts responses
/// </summary>
public class GetAllCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllCarts operations
    /// </summary>
    public GetAllCartsProfile()
    {
        CreateMap<Cart, GetAllCartsItemResult>();
        CreateMap<CartProduct, GetAllCartsProductResult>();
    }
} 