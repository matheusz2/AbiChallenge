using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Profile for mapping between Sale entity and UpdateSaleResult
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale operation
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Domain.Entities.Sale>();
        CreateMap<UpdateSaleItemCommand, Domain.Entities.SaleItem>();
        CreateMap<Domain.Entities.Sale, UpdateSaleResult>();
        CreateMap<Domain.Entities.SaleItem, UpdateSaleItemResult>();
    }
} 