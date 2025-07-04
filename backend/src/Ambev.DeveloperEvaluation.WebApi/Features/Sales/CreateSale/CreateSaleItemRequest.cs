using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale item in the system.
    /// </summary>
    public class CreateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the product identifier. Must be a non-empty GUID.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product. Must be greater than zero.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product. Must be greater than zero.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}