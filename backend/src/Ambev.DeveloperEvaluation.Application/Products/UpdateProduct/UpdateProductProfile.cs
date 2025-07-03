using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<Product, UpdateProductResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new UpdateProductRatingResult 
            { 
                Rate = src.RatingRate, 
                Count = src.RatingCount 
            }));
    }
} 