using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.RatingRate, opt => opt.MapFrom(src => src.Rating.Rate))
            .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Rating.Count))
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Product, CreateProductResult>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new CreateProductRatingResult 
            { 
                Rate = src.RatingRate, 
                Count = src.RatingCount 
            }));
    }
} 