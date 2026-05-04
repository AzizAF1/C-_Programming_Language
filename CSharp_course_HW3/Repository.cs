using System;
using System.Collections.Generic;

public class Repository<T> where T : IEntity
{
    private readonly Dictionary<int, T> _items = new Dictionary<int, T>();

    public int Count
    {
        get { return _items.Count; }
    }

    public void Add(T item)
    {
        if (_items.ContainsKey(item.Id))
        {
            throw new InvalidOperationException("Item with the same Id already exists.");
        }

        _items.Add(item.Id, item);
    }

    public bool Remove(int id)
    {
        return _items.Remove(id);
    }

    public T? GetById(int id)
    {
        T? item;
        if (_items.TryGetValue(id, out item))
        {
            return item;
        }

        return default;
    }

    public IReadOnlyList<T> GetAll()
    {
        List<T> result = new List<T>();

        foreach (KeyValuePair<int, T> pair in _items)
        {
            result.Add(pair.Value);
        }

        return result;
    }

    public IReadOnlyList<T> Find(Predicate<T> predicate)
    {
        List<T> result = new List<T>();

        foreach (KeyValuePair<int, T> pair in _items)
        {
            if (predicate(pair.Value))
            {
                result.Add(pair.Value);
            }
        }

        return result;
    }
}
