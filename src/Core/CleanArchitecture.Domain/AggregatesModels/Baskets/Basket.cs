using CleanArchitecture.Domain.Common;


namespace CleanArchitecture.Domain.AggregatesModels.Baskets;

public sealed class Basket : BaseEntityRoot
{
    private readonly List<BasketProductItem> _bastketProductItems = new();

    private Basket()
    {
    }

    private Basket(
        Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<BasketProductItem> BasketProductItems => _bastketProductItems;

    public decimal TotalPrice { get => BasketProductItems.Sum(b => b.Quantity * b.Price); }

    public static Basket Create(Guid userId)
    {
        Basket basket = new Basket(userId);
        return basket;
    }

    public static Basket Create()
    {
        Basket basket = new Basket();
        return basket;
    }

    public void AddBasketProductItem(Guid productId, int quantity, decimal price, string productName)
    {
        if (quantity <= 0) 
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        var basketProductItem = _bastketProductItems.FirstOrDefault(b => b.ProductId == productId);
        if (basketProductItem is null)
        {
            _bastketProductItems.Add(BasketProductItem.Create(this, productId, quantity, price, productName));
        }
        else
        {
            basketProductItem.Update(quantity);
        }
    }

    public void RemoveBasketProductItem(Guid productId)
    {
        var basketProductItem = _bastketProductItems.FirstOrDefault(x => x.ProductId == productId);
        if (basketProductItem is not null)
            _bastketProductItems.Remove(basketProductItem);
    }

    public void Clear()
    {
        _bastketProductItems.Clear();
    }
}
