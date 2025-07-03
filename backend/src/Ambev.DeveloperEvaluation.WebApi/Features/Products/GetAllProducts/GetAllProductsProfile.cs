using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Profile for mapping GetAllProducts requests
/// </summary>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllProducts
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<GetAllProductsRequest, GetAllProductsCommand>()
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => new ProductFilter
            {
                Title = src.title,
                Category = src.category,
                Price = src.price,
                Description = src.description,
                MinPrice = src._minPrice,
                MaxPrice = src._maxPrice,
                MinRating = src._minRating,
                MaxRating = src._maxRating,
                MinRatingCount = src._minRatingCount,
                MaxRatingCount = src._maxRatingCount
            }));

        CreateMap<GetAllProductsItemResult, GetProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRating 
            { 
                Rate = src.Rating.Rate, 
                Count = src.Rating.Count 
            }));
    }
} 