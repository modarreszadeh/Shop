using Api.Constants;
using Api.Exceptions;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductEntity = Data.Entities.Product;

namespace Api.Services.Product;

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public ProductService(AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }

    public async Task<List<ProductEntity>> GetListAsync(CancellationToken cancellationToken)
    {
        var productListCache = _memoryCache.Get<List<ProductEntity>>(CacheKeyConstants.ProductList);

        if (productListCache != default)
            return productListCache;

        var productList = await _dbContext.Set<ProductEntity>().AsNoTracking().ToListAsync(cancellationToken);

        await _setProductListCacheAsync(cancellationToken);

        return productList;
    }

    public async Task<ProductEntity> GetByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var productListCache = _memoryCache.Get<List<ProductEntity>>(CacheKeyConstants.ProductList);

        var productCache = productListCache?.SingleOrDefault(p => p.Id == productId);

        if (productCache != default)
            return productCache;

        var product = await _dbContext.Set<ProductEntity>().AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        if (product == default)
            throw new EntityNotFoundException<ProductEntity>(productId);

        await _setProductListCacheAsync(cancellationToken);

        return product;
    }

    public async Task CreateAsync(ProductEntity product, CancellationToken cancellationToken)
    {
        var isExistTitle = await _dbContext.Set<ProductEntity>()
            .AnyAsync(x => x.Title == product.Title, cancellationToken);

        if (isExistTitle)
            throw new UserFriendlyException("Title has exist in database");

        await _dbContext.Set<ProductEntity>().AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _setProductListCacheAsync(cancellationToken);
    }

    public async Task IncreaseInventoryCountAsync(int productId, int count, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Set<ProductEntity>()
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

        if (product == default)
            throw new EntityNotFoundException<ProductEntity>(productId);

        product.IncreaseInventoryCount(count);

        _dbContext.Set<ProductEntity>().Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _setProductListCacheAsync(cancellationToken);
    }

    private async Task _setProductListCacheAsync(CancellationToken cancellationToken)
    {
        var productList = await _dbContext.Set<ProductEntity>().ToListAsync(cancellationToken);

        _memoryCache.Set(CacheKeyConstants.ProductList, productList);
    }
}