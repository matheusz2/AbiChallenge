using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Profile for mapping CreateCart requests and responses
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
        CreateMap<CreateCartProductRequest, CreateCartProductCommand>();

        CreateMap<CreateCartResult, CreateCartResponse>();
        CreateMap<CreateCartProductResult, CreateCartProductResponse>();
    }
} 