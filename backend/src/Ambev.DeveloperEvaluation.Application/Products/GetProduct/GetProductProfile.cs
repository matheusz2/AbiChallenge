using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingResult 
            { 
                Rate = src.RatingRate, 
                Count = src.RatingCount 
            }));
    }
} 