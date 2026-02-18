using CleanArchitecture.Application.Common.ApplicationServices.Auth;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Baskets.Specs;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Services.Checkout;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.CheckoutBasket;

public class CheckoutCommandHandler : ICommandHandler<CheckoutCommand, Guid>
{
    private readonly CheckoutService _checkoutService;
    private readonly IBasketRepository _basketRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    
    public CheckoutCommandHandler(
        CheckoutService checkoutService, 
        IBasketRepository basketRepository, 
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser)
    {
        _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Result<Guid>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _currentUser.GetUserId();

        // Note: Validators should catch invalid inputs before this, but domain will also guard.
        UserInformation userInfomation = new UserInformation(
            request.UserInformation.Name,
            new Phone(request.UserInformation.Phone),
            new Address(request.UserInformation.Address.Street, request.UserInformation.Address.City));

        Basket? basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdWithBasketItemAndProductSpec(userId), cancellationToken);

        if (basket is null || !basket.BasketProductItems.Any())
            return Result.Failure<Guid>(BasketErrors.BasketProductItemEmpty);

        // Domain Logic: Create Order, Clear Basket
        Order order = _checkoutService.Checkout(basket, userInfomation);

        // Persistence Logic
        await _orderRepository.AddAsync(order, cancellationToken);
        // Update Basket (cleared)
        // await _basketRepository.UpdateAsync(basket, cancellationToken); // EF Core tracking might handle this if loaded?
        // Usually need to save changes on UnitOfWork.

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
