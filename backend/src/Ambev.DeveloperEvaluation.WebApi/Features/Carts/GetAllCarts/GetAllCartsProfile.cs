using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

/// <summary>
/// Profile for mapping GetAllCarts requests and responses
/// </summary>
public class GetAllCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllCarts
    /// </summary>
    public GetAllCartsProfile()
    {
        CreateMap<GetAllCartsRequest, GetAllCartsCommand>();

        CreateMap<GetAllCartsResult, GetAllCartsResponse>();
        CreateMap<GetAllCartsItemResult, GetAllCartsItemResponse>();
        CreateMap<GetAllCartsProductResult, GetAllCartsProductResponse>();
    }
} 