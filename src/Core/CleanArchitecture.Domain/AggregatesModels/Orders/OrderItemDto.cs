namespace CleanArchitecture.Domain.AggregatesModels.Orders;

public record OrderItemDto(Guid ProductId, int Quantity, decimal Price);
