using System;
using System.Collections.Generic;

public static class CollectionUtils
{
    public static List<T> Distinct<T>(List<T> source)
    {
        List<T> result = new List<T>();

        foreach (T item in source)
        {
            if (!result.Contains(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public static Dictionary<TKey, List<TValue>> GroupBy<TValue, TKey>(
        List<TValue> source,
        Func<TValue, TKey> keySelector) where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> result = new Dictionary<TKey, List<TValue>>();

        foreach (TValue item in source)
        {
            TKey key = keySelector(item);

            if (!result.ContainsKey(key))
            {
                result.Add(key, new List<TValue>());
            }

            result[key].Add(item);
        }

        return result;
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        Dictionary<TKey, TValue> first,
        Dictionary<TKey, TValue> second,
        Func<TValue, TValue, TValue> conflictResolver) where TKey : notnull
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

        foreach (KeyValuePair<TKey, TValue> pair in first)
        {
            result.Add(pair.Key, pair.Value);
        }

        foreach (KeyValuePair<TKey, TValue> pair in second)
        {
            if (result.ContainsKey(pair.Key))
            {
                result[pair.Key] = conflictResolver(result[pair.Key], pair.Value);
            }
            else
            {
                result.Add(pair.Key, pair.Value);
            }
        }

        return result;
    }

    public static T MaxBy<T, TKey>(List<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
    {
        if (source.Count == 0)
        {
            throw new InvalidOperationException("Cannot find maximum element in an empty list.");
        }

        T maxItem = source[0];
        TKey maxValue = selector(maxItem);

        for (int i = 1; i < source.Count; i++)
        {
            T currentItem = source[i];
            TKey currentValue = selector(currentItem);

            if (currentValue.CompareTo(maxValue) > 0)
            {
                maxItem = currentItem;
                maxValue = currentValue;
            }
        }

        return maxItem;
    }
}
