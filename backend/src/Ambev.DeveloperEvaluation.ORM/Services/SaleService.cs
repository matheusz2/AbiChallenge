using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.ORM.Services;

/// <summary>
/// Implementação de ISaleService para regras de negócio de vendas
/// </summary>
public class SaleService : ISaleService
{
    public async Task ApplyBusinessRulesAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        if (sale == null)
            throw new ArgumentNullException(nameof(sale));

        if (!ValidateBusinessRules(sale))
            throw new InvalidOperationException("A venda não atende às regras de negócio.");

        var subtotal = CalculateSubtotal(sale);
        var discountPercentage = CalculateDiscountPercentage(sale.Items.Count);
        var discountAmount = CalculateDiscountAmount(subtotal, discountPercentage);
        var total = CalculateTotal(subtotal, discountAmount);

        sale.Subtotal = subtotal;
        sale.DiscountPercentage = discountPercentage;
        sale.DiscountAmount = discountAmount;
        sale.Total = total;

        await Task.CompletedTask;
    }

    public decimal CalculateDiscountPercentage(int itemsCount)
    {
        if (itemsCount >= 10 && itemsCount <= 20)
            return 20m;
        if (itemsCount >= 4)
            return 10m;
        return 0m;
    }

    public decimal CalculateSubtotal(Sale sale)
    {
        return sale.Items.Sum(item => item.Quantity * item.UnitPrice);
    }

    public decimal CalculateDiscountAmount(decimal subtotal, decimal discountPercentage)
    {
        return Math.Round(subtotal * (discountPercentage / 100m), 2);
    }

    public decimal CalculateTotal(decimal subtotal, decimal discountAmount)
    {
        return Math.Round(subtotal - discountAmount, 2);
    }

    public bool ValidateBusinessRules(Sale sale)
    {
        if (sale.Items == null || sale.Items.Count == 0)
            return false;
        if (sale.Items.Count > 20)
            return false;
        if (sale.Items.Any(item => item.Quantity <= 0 || item.UnitPrice <= 0))
            return false;
        return true;
    }
} 