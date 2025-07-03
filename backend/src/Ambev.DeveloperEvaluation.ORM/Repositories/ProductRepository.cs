using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        product.Id = Guid.NewGuid();
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(int page, int pageSize, string? orderBy = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();
        
        // Aplicar ordenação
        query = ApplyOrdering(query, orderBy);
        
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetByCategoryAsync(string category, int page, int pageSize, string? orderBy = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Where(p => p.Category.ToLower() == category.ToLower());
        
        // Aplicar ordenação
        query = ApplyOrdering(query, orderBy);
        
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static IQueryable<Product> ApplyOrdering(IQueryable<Product> query, string? orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return query.OrderBy(p => p.Title);
        }

        return orderBy.ToLower() switch
        {
            "title" or "title_asc" => query.OrderBy(p => p.Title),
            "title_desc" => query.OrderByDescending(p => p.Title),
            "price" or "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "category" or "category_asc" => query.OrderBy(p => p.Category),
            "category_desc" => query.OrderByDescending(p => p.Category),
            "rating" or "rating_asc" => query.OrderBy(p => p.RatingRate),
            "rating_desc" => query.OrderByDescending(p => p.RatingRate),
            "id" or "id_asc" => query.OrderBy(p => p.Id),
            "id_desc" => query.OrderByDescending(p => p.Id),
            _ => query.OrderBy(p => p.Title)
        };
    }
} 