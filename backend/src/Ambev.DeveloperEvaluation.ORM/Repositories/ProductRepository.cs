using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Models;
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

    public async Task<List<Product>> GetAllAsync(int page, int pageSize, string? orderBy = null, ProductFilter? filter = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();
        
        // Aplicar filtros
        query = ApplyFilters(query, filter);
        
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

    /// <summary>
    /// Applies filtering to the query based on the filter criteria
    /// </summary>
    /// <param name="query">The query to apply filters to</param>
    /// <param name="filter">The filter criteria</param>
    /// <returns>The filtered query</returns>
    private static IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilter? filter)
    {
        if (filter == null) return query;

        // Title filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Title))
        {
            if (filter.HasTitleWildcard)
            {
                var titlePattern = filter.GetTitleWithoutWildcard();
                if (filter.Title.StartsWith("*") && filter.Title.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(p => p.Title.ToLower().Contains(titlePattern.ToLower()));
                }
                else if (filter.Title.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(p => p.Title.ToLower().EndsWith(titlePattern.ToLower()));
                }
                else if (filter.Title.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(p => p.Title.ToLower().StartsWith(titlePattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(p => p.Title.ToLower() == filter.Title.ToLower());
            }
        }

        // Category filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Category))
        {
            if (filter.HasCategoryWildcard)
            {
                var categoryPattern = filter.GetCategoryWithoutWildcard();
                if (filter.Category.StartsWith("*") && filter.Category.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(p => p.Category.ToLower().Contains(categoryPattern.ToLower()));
                }
                else if (filter.Category.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(p => p.Category.ToLower().EndsWith(categoryPattern.ToLower()));
                }
                else if (filter.Category.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(p => p.Category.ToLower().StartsWith(categoryPattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(p => p.Category.ToLower() == filter.Category.ToLower());
            }
        }

        // Description filters (with wildcard support)
        if (!string.IsNullOrEmpty(filter.Description))
        {
            if (filter.HasDescriptionWildcard)
            {
                var descriptionPattern = filter.GetDescriptionWithoutWildcard();
                if (filter.Description.StartsWith("*") && filter.Description.EndsWith("*"))
                {
                    // Contains
                    query = query.Where(p => p.Description.ToLower().Contains(descriptionPattern.ToLower()));
                }
                else if (filter.Description.StartsWith("*"))
                {
                    // Ends with
                    query = query.Where(p => p.Description.ToLower().EndsWith(descriptionPattern.ToLower()));
                }
                else if (filter.Description.EndsWith("*"))
                {
                    // Starts with
                    query = query.Where(p => p.Description.ToLower().StartsWith(descriptionPattern.ToLower()));
                }
            }
            else
            {
                // Exact match
                query = query.Where(p => p.Description.ToLower() == filter.Description.ToLower());
            }
        }

        // Exact price filter
        if (filter.Price.HasValue)
        {
            query = query.Where(p => p.Price == filter.Price.Value);
        }

        // Price range filters
        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        // Rating range filters
        if (filter.MinRating.HasValue)
        {
            query = query.Where(p => p.RatingRate >= filter.MinRating.Value);
        }

        if (filter.MaxRating.HasValue)
        {
            query = query.Where(p => p.RatingRate <= filter.MaxRating.Value);
        }

        // Rating count range filters
        if (filter.MinRatingCount.HasValue)
        {
            query = query.Where(p => p.RatingCount >= filter.MinRatingCount.Value);
        }

        if (filter.MaxRatingCount.HasValue)
        {
            query = query.Where(p => p.RatingCount <= filter.MaxRatingCount.Value);
        }

        return query;
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