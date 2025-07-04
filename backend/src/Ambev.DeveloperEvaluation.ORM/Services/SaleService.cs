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
        var totalDiscount = 0m;
        var totalWithDiscount = 0m;

        // Calcular desconto por produto individual
        foreach (var item in sale.Items)
        {
            var itemTotal = item.Quantity * item.UnitPrice;
            var discountPercentage = CalculateDiscountPercentage(item.Quantity);
            var itemDiscount = CalculateDiscountAmount(itemTotal, discountPercentage);
            
            totalDiscount += itemDiscount;
            totalWithDiscount += (itemTotal - itemDiscount);
            
            // Atualizar o item com o desconto calculado
            item.TotalPrice = itemTotal - itemDiscount;
        }

        var total = CalculateTotal(subtotal, totalDiscount);

        sale.Subtotal = subtotal;
        sale.DiscountPercentage = subtotal > 0 ? (totalDiscount / subtotal) * 100 : 0;
        sale.DiscountAmount = totalDiscount;
        sale.Total = total;

        await Task.CompletedTask;
    }

    public decimal CalculateDiscountPercentage(int quantity)
    {
        if (quantity >= 10 && quantity <= 20)
            return 20m;
        if (quantity >= 4)
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
            
        // Verificar se há mais de 20 itens na venda
        if (sale.Items.Count > 20)
            return false;
            
        // Verificar se há produtos com mais de 20 unidades
        if (sale.Items.Any(item => item.Quantity > 20))
            return false;
            
        // Verificar se há produtos com quantidade zero ou negativa
        if (sale.Items.Any(item => item.Quantity <= 0))
            return false;
            
        // Verificar se há produtos com preço zero ou negativo
        if (sale.Items.Any(item => item.UnitPrice <= 0))
            return false;
            
        return true;
    }
} 