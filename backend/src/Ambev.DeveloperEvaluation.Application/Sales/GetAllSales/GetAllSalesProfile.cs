using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Profile for mapping between Sale entity and GetAllSalesResult
/// </summary>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllSales operation
    /// </summary>
    public GetAllSalesProfile()
    {
        CreateMap<Domain.Entities.Sale, GetAllSalesItemResult>()
            .ForMember(dest => dest.ItemsCount, opt => opt.MapFrom(src => src.Items.Count));
    }
} 