namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Response model for sale items in GetSale operation
    /// </summary>
    public class GetSaleItemResponse
    {
        /// <summary>
        /// The unique identifier of the sale item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the product
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The quantity of the product
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The total price for this item (quantity * unit price)
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
