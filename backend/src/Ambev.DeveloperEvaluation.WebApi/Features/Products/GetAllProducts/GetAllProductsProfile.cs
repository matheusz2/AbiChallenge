using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

public class GetAllProductsProfile : Profile
{
    public GetAllProductsProfile()
    {
        CreateMap<GetAllProductsRequest, GetAllProductsCommand>()
            .ConstructUsing(src => new GetAllProductsCommand(src.Page, src.Size, src.Order));

        CreateMap<GetAllProductsItemResult, GetProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRating 
            { 
                Rate = src.Rating.Rate, 
                Count = src.Rating.Count 
            }));
    }
} 