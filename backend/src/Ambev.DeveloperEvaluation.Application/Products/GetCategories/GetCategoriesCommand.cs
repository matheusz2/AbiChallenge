using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories;

public record GetCategoriesCommand : IRequest<List<string>>; 