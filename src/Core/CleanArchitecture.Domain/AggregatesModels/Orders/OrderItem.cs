using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Orders;

public class OrderItem : BaseEntity
{
    private OrderItem(
        Guid orderId,
        Guid productId,
        int quantity,
        decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public virtual Product? Product { get; init; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public static OrderItem Create(Guid orderId, Guid productId, int quantity, decimal price)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));

        OrderItem orderItem = new OrderItem(
            orderId,
            productId,
            quantity,
            price);

        return orderItem;
    }
}
