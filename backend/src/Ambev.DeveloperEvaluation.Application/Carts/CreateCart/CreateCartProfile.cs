using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Profile for mapping between Cart entities and CreateCart commands/results
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart operations
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CreateCartProductCommand, CartProduct>()
            .ForMember(dest => dest.CartId, opt => opt.Ignore())
            .ForMember(dest => dest.Cart, opt => opt.Ignore());

        CreateMap<Cart, CreateCartResult>();

        CreateMap<CartProduct, CreateCartProductResult>();
    }
} 