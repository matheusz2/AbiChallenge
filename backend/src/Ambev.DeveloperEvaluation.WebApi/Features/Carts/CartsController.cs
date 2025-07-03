using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing cart operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CartsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="request">The cart creation request</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartCommand>(request);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<CreateCartResponse>(response);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = result
        });
    }

    /// <summary>
    /// Retrieves all carts with pagination
    /// </summary>
    /// <param name="request">The pagination and filtering parameters</param>
    /// <returns>Paginated list of carts</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllCarts([FromQuery] GetAllCartsRequest request)
    {
        var command = _mapper.Map<GetAllCartsCommand>(request);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<GetAllCartsResponse>(response);

        return Ok(new PaginatedResponse<GetAllCartsItemResponse>
        {
            Success = true,
            Message = "Carts retrieved successfully",
            Data = result.Data,
            TotalCount = result.TotalCount,
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages
        });
    }

    /// <summary>
    /// Retrieves a specific cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <returns>The cart details</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCart([FromRoute] Guid id)
    {
        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetCartCommand(id);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<GetCartResponse>(response);

        return base.Ok(new ApiResponseWithData<GetCartResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = result
        });
    }

    /// <summary>
    /// Updates an existing cart
    /// </summary>
    /// <param name="id">The unique identifier of the cart to update</param>
    /// <param name="request">The cart update request</param>
    /// <returns>The updated cart details</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateCart([FromBody] UpdateCartRequest request)
    {
        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartCommand>(request);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<UpdateCartResponse>(response);

        return base.Ok(new ApiResponseWithData<UpdateCartResponse>
        {
            Success = true,
            Message = "Cart updated successfully",
            Data = result
        });
    }

    /// <summary>
    /// Deletes a cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <returns>Confirmation of deletion</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new DeleteCartCommand(id);
        var response = await _mediator.Send(command);

        return base.Ok(new ApiResponse
        {
            Success = response.Success,
            Message = response.Success ? "Cart deleted successfully" : "Cart not found"
        });
    }
} 