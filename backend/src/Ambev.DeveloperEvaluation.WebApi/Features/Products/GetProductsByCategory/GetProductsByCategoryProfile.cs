using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryProfile : Profile
{
    public GetProductsByCategoryProfile()
    {
        CreateMap<GetProductsByCategoryRequest, GetProductsByCategoryCommand>()
            .ConstructUsing(src => new GetProductsByCategoryCommand(src.Category, src.Page, src.Size, src.Order));

        CreateMap<GetProductsByCategoryItemResult, GetProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRating 
            { 
                Rate = src.Rating.Rate, 
                Count = src.Rating.Count 
            }));
    }
} 