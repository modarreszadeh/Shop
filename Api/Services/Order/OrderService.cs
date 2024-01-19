using Api.Constants;
using Api.Exceptions;
using Api.Services.Product;
using Data.DbContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductEntity = Data.Entities.Product;
using OrderEntity = Data.Entities.Order;

namespace Api.Services.Order;

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductService _productService;

    public OrderService(AppDbContext dbContext, IMemoryCache memoryCache, IProductService productService)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
        _productService = productService;
    }

    public async Task<bool> CheckoutAsync(int userId, int productId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<User>().AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == default)
            throw new EntityNotFoundException<User>(userId);

        var product = await _productService.GetByIdAsync(productId, cancellationToken);

        if (product.InventoryCount < 1)
            throw new UserFriendlyException("product inventory count is not enough");

        var order = new OrderEntity(userId, productId);

        product.DecreaseInventoryCount(1);

        _dbContext.Set<ProductEntity>().Update(product);
        await _dbContext.Set<OrderEntity>().AddAsync(order, cancellationToken);

        var rowAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        _memoryCache.Remove(CacheKeyConstants.ProductList);

        return rowAffected == 2;
    }
}