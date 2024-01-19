using ProductEntity = Data.Entities.Product;

namespace Api.Services.Product;

public interface IProductService
{
    Task<List<ProductEntity>> GetListAsync(CancellationToken cancellationToken);
    Task<ProductEntity> GetByIdAsync(int productId, CancellationToken cancellationToken);
    Task CreateAsync(ProductEntity product, CancellationToken cancellationToken);
    Task IncreaseInventoryCountAsync(int productId, int count, CancellationToken cancellationToken);
}