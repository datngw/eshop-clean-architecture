using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Services.Checkout;

public class CheckoutService : IDomainService
{
    public Order Checkout(Basket basket, UserInformation userInfomation)
    {
        var orderItems = basket.BasketProductItems.Select(x => new OrderItemDto(x.ProductId, x.Quantity, x.Product.Price));
        Order order = Order.Create(basket.UserId, userInfomation, orderItems);
        basket.Clear();

        return order;
    }
}
