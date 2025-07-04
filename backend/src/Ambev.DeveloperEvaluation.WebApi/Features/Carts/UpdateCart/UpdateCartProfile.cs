using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Profile for mapping UpdateCart requests and responses
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>();
        CreateMap<UpdateCartProductRequest, UpdateCartProductCommand>();

        CreateMap<UpdateCartResult, UpdateCartResponse>();
        CreateMap<UpdateCartProductResult, UpdateCartProductResponse>();
    }
}