using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;

public class GetCategoriesResponse
{
    public List<string> Categories { get; set; } = new();
} 