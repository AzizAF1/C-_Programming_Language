public class User : IEntity
{
    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }

    public override string ToString()
    {
        return $"User: Id = {Id}, Name = {Name}";
    }
}
