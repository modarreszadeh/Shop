namespace Data.Entities;

public class User : IEntity
{
    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public User(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; } = default!;
}