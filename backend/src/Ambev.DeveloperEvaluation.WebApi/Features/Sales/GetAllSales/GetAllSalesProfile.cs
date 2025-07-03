using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Profile for mapping GetAllSales feature requests to commands
/// </summary>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllSales feature
    /// </summary>
    public GetAllSalesProfile()
    {
        CreateMap<GetAllSalesRequest, GetAllSalesCommand>();
        CreateMap<GetAllSalesItemResult, GetSaleResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();
    }
} 