using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Cart entities and UpdateCart commands/results
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart operations
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartCommand, Cart>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateCartProductCommand, CartProduct>()
            .ForMember(dest => dest.CartId, opt => opt.Ignore())
            .ForMember(dest => dest.Cart, opt => opt.Ignore());

        CreateMap<Cart, UpdateCartResult>();

        CreateMap<CartProduct, UpdateCartProductResult>();
    }
}