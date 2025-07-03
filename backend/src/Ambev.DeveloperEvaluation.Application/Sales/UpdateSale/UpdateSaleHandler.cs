using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ISaleService _saleService;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="saleService">The sale service for business logic</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ISaleService saleService)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _saleService = saleService;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} not found");

        // Atualize campos simples
        existingSale.CustomerId = command.CustomerId;
        existingSale.BranchId = command.BranchId;

        // Atualize a coleção de itens
        // Remover itens que não estão mais na lista
        existingSale.Items.RemoveAll(item => !command.Items.Any(ci => ci.Id == item.Id));

        // Atualizar ou adicionar itens
        foreach (var itemCommand in command.Items)
        {
            var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == itemCommand.Id);
            if (existingItem != null)
            {
                // Atualizar campos do item existente
                existingItem.ProductId = itemCommand.ProductId;
                existingItem.Quantity = itemCommand.Quantity;
                existingItem.UnitPrice = itemCommand.UnitPrice;
                // Atualize outros campos se necessário
            }
            else
            {
                // Adicionar novo item
                existingSale.Items.Add(new SaleItem
                {
                    ProductId = itemCommand.ProductId,
                    Quantity = itemCommand.Quantity,
                    UnitPrice = itemCommand.UnitPrice,
                    CreatedAt = DateTime.UtcNow
                    // Adicione outros campos se necessário
                });
            }
        }

        await _saleService.ApplyBusinessRulesAsync(existingSale, cancellationToken);

        await _saleRepository.UpdateAsync(existingSale, cancellationToken);
        var result = _mapper.Map<UpdateSaleResult>(existingSale);

        return result;
    }
} 