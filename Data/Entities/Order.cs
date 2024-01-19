namespace Data.Entities;

public class Order : IEntity
{
    public Order(int userId, int productId)
    {
        UserId = userId;
        ProductId = productId;
    }

    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public User Buyer { get; } = default!;
    public Product Product { get; } = default!;
}