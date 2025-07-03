using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsProfile : Profile
{
    public GetAllProductsProfile()
    {
        CreateMap<Product, GetAllProductsItemResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingResult 
            { 
                Rate = src.RatingRate, 
                Count = src.RatingCount 
            }));
    }
} 