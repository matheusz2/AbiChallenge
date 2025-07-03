using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[ApiController]
[Route("api/products")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieve all products with pagination
    /// </summary>
    /// <param name="request">Pagination and ordering request</param>
    /// <returns>Paginated list of products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), 200)]
    public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsRequest request)
    {
        var command = _mapper.Map<GetAllProductsCommand>(request);
        var result = await _mediator.Send(command);

        var response = new PaginatedResponse<GetProductResponse>
        {
            Data = _mapper.Map<List<GetProductResponse>>(result.Data),
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalCount = result.TotalItems
        };

        return Ok(response);
    }

    /// <summary>
    /// Retrieve a specific product by ID
    /// </summary>
    /// <param name="id">Product unique identifier</param>
    /// <returns>Product details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var command = new GetProductCommand(id);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<GetProductResponse>(result);

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product creation request</param>
    /// <returns>Created product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), 201)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<CreateProductResponse>(result);

        return CreatedAtAction(nameof(GetProduct), new { id = response.Id }, 
            new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = response
            });
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Product unique identifier</param>
    /// <param name="request">Product update request</param>
    /// <returns>Updated product details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateProductCommand>(request);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<UpdateProductResponse>(result);

        return Ok(new ApiResponseWithData<UpdateProductResponse>
        {
            Success = true,
            Message = "Product updated successfully",
            Data = response
        });
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">Product unique identifier</param>
    /// <returns>Deletion confirmation</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProductCommand(id);
        var deleted = await _mediator.Send(command);

        if (!deleted)
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Product not found"
            });

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Product deleted successfully"
        });
    }

    /// <summary>
    /// Get all product categories
    /// </summary>
    /// <returns>List of available categories</returns>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCategoriesResponse>), 200)]
    public async Task<IActionResult> GetCategories()
    {
        var command = new GetCategoriesCommand();
        var categories = await _mediator.Send(command);

        var response = new GetCategoriesResponse
        {
            Categories = categories
        };

        return Ok(new ApiResponseWithData<GetCategoriesResponse>
        {
            Success = true,
            Message = "Categories retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Get products by category with pagination
    /// </summary>
    /// <param name="category">Category name</param>
    /// <param name="request">Pagination and ordering request</param>
    /// <returns>Paginated list of products in the specified category</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), 200)]
    public async Task<IActionResult> GetProductsByCategory(string category, [FromQuery] GetProductsByCategoryRequest request)
    {
        request.Category = category;
        var command = _mapper.Map<GetProductsByCategoryCommand>(request);
        var result = await _mediator.Send(command);

        var response = new PaginatedResponse<GetProductResponse>
        {
            Data = _mapper.Map<List<GetProductResponse>>(result.Data),
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalCount = result.TotalItems
        };

        return Ok(response);
    }
} 