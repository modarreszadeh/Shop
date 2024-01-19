using Data.Exceptions;

namespace Data.Entities;

public class Product : IEntity
{
    public Product()
    {
    }

    public Product(string title, int inventoryCount, int price, int discount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        if (title.Length > 40)
            throw new EntityException("title should be less than 40 character");

        Title = title;
        InventoryCount = inventoryCount;
        Price = price;
        Discount = discount;
    }

    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int InventoryCount { get; set; }
    public int Price { get; set; }
    public int Discount { get; set; }

    public ICollection<Order> Orders { get; set; } = default!;


    public void IncreaseInventoryCount(int count)
    {
        InventoryCount += count;
    }

    public void DecreaseInventoryCount(int count)
    {
        if (InventoryCount - count < 0)
            throw new EntityException($"count value should not be greater than inventory count");

        InventoryCount -= count;
    }
}