using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public record GetProductCommand(Guid Id) : IRequest<GetProductResult>; 