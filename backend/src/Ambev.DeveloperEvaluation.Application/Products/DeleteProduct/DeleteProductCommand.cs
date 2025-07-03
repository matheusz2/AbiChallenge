using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<bool>; 