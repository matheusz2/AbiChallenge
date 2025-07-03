namespace Ambev.DeveloperEvaluation.Domain.Models;

/// <summary>
/// Filter criteria for products
/// </summary>
public class ProductFilter
{
    public string? Title { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public double? MinRating { get; set; }
    public double? MaxRating { get; set; }
    public int? MinRatingCount { get; set; }
    public int? MaxRatingCount { get; set; }

    /// <summary>
    /// Checks if the title contains wildcard characters
    /// </summary>
    public bool HasTitleWildcard => !string.IsNullOrEmpty(Title) && (Title.StartsWith("*") || Title.EndsWith("*"));

    /// <summary>
    /// Checks if the category contains wildcard characters
    /// </summary>
    public bool HasCategoryWildcard => !string.IsNullOrEmpty(Category) && (Category.StartsWith("*") || Category.EndsWith("*"));

    /// <summary>
    /// Checks if the description contains wildcard characters
    /// </summary>
    public bool HasDescriptionWildcard => !string.IsNullOrEmpty(Description) && (Description.StartsWith("*") || Description.EndsWith("*"));

    /// <summary>
    /// Gets the title without wildcard characters
    /// </summary>
    public string GetTitleWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Title)) return string.Empty;
        return Title.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the category without wildcard characters
    /// </summary>
    public string GetCategoryWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Category)) return string.Empty;
        return Category.Replace("*", "").Trim();
    }

    /// <summary>
    /// Gets the description without wildcard characters
    /// </summary>
    public string GetDescriptionWithoutWildcard()
    {
        if (string.IsNullOrEmpty(Description)) return string.Empty;
        return Description.Replace("*", "").Trim();
    }
} 