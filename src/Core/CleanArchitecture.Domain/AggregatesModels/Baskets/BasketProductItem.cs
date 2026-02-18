using CleanArchitecture.Domain.AggregatesModels.Products;


namespace CleanArchitecture.Domain.AggregatesModels.Baskets;

public class BasketProductItem : BaseEntity
{
    private BasketProductItem(
        Guid basketId,
        Guid productId,
        int quantity,
        decimal price,
        string productName)
    {
        BasketId = basketId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
    }

    public Guid BasketId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public string ProductName { get; private set; }
    public virtual Product? Product { get; private set; }

    internal static BasketProductItem Create(
        Basket basket,
        Guid productId,
        int quantity,
        decimal price,
        string productName)
    {
        BasketProductItem basketProductItem = new BasketProductItem(
            basket.Id,
            productId,
            quantity,
            price,
            productName);

        return basketProductItem;
    }

    internal void Update(int quantity)
    {
        Quantity = Quantity + quantity;
    }

}
