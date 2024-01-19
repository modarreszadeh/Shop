namespace Api.Services.Order;

public interface IOrderService
{
    Task<bool> CheckoutAsync(int userId, int productId, CancellationToken cancellationToken);
}