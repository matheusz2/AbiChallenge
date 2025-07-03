using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new CreateProductRatingCommand 
            { 
                Rate = src.Rating.Rate, 
                Count = src.Rating.Count 
            }));

        CreateMap<CreateProductResult, CreateProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new CreateProductRating 
            { 
                Rate = src.Rating.Rate, 
                Count = src.Rating.Count 
            }));
    }
} 