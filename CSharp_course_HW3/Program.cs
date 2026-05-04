using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Repository<Product> demo");
        Console.WriteLine();

        Repository<Product> productRepository = new Repository<Product>();
        productRepository.Add(new Product(1, "Laptop", 1500m));
        productRepository.Add(new Product(2, "Mouse", 25m));
        productRepository.Add(new Product(3, "Monitor", 350m));
        productRepository.Add(new Product(4, "Phone", 1200m));

        Console.WriteLine("All products:");
        PrintList(productRepository.GetAll());

        Console.WriteLine();
        Console.WriteLine("Product with Id = 3:");
        Product? productById = productRepository.GetById(3);
        PrintNullable(productById);

        Console.WriteLine();
        Console.WriteLine("Products with Price > 1000:");
        IReadOnlyList<Product> expensiveProducts = productRepository.Find(product => product.Price > 1000m);
        PrintList(expensiveProducts);

        Console.WriteLine();
        Console.WriteLine("Trying to add a product with duplicate Id = 1:");
        try
        {
            productRepository.Add(new Product(1, "Duplicate laptop", 999m));
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"Caught exception: {exception.Message}");
        }

        Console.WriteLine();
        bool removed = productRepository.Remove(2);
        Console.WriteLine($"Remove product with Id = 2: {removed}");
        Console.WriteLine($"Product count: {productRepository.Count}");

        Console.WriteLine();
        Console.WriteLine("Repository<User> demo");
        Console.WriteLine();

        Repository<User> userRepository = new Repository<User>();
        userRepository.Add(new User(1, "Alice"));
        userRepository.Add(new User(2, "Bob"));
        userRepository.Add(new User(3, "Aziz"));

        Console.WriteLine("All users:");
        PrintList(userRepository.GetAll());

        Console.WriteLine();
        Console.WriteLine("User with Id = 2:");
        User? userById = userRepository.GetById(2);
        PrintNullable(userById);

        Console.WriteLine();
        Console.WriteLine("CollectionUtils demo");
        Console.WriteLine();

        List<int> numbers = new List<int> { 1, 2, 2, 3, 1, 4, 3 };
        List<int> distinctNumbers = CollectionUtils.Distinct(numbers);
        Console.WriteLine("Distinct integers:");
        PrintList(distinctNumbers);

        Console.WriteLine();
        List<string> wordsWithDuplicates = new List<string> { "cat", "dog", "cat", "bird", "dog", "fish" };
        List<string> distinctWords = CollectionUtils.Distinct(wordsWithDuplicates);
        Console.WriteLine("Distinct strings:");
        PrintList(distinctWords);

        Console.WriteLine();
        List<string> words = new List<string> { "sun", "tree", "river", "sky", "cloud", "book" };
        Dictionary<int, List<string>> groupedWords = CollectionUtils.GroupBy(words, word => word.Length);
        Console.WriteLine("Group strings by length:");
        PrintDictionaryOfLists(groupedWords);

        Console.WriteLine();
        Dictionary<string, int> firstScores = new Dictionary<string, int>
        {
            { "Alice", 10 },
            { "Bob", 5 },
            { "Diana", 8 }
        };
        Dictionary<string, int> secondScores = new Dictionary<string, int>
        {
            { "Bob", 7 },
            { "Aziz", 3 },
            { "Diana", 2 }
        };
        Dictionary<string, int> mergedScores = CollectionUtils.Merge(
            firstScores,
            secondScores,
            (oldValue, newValue) => oldValue + newValue);
        Console.WriteLine("Merged dictionaries with sum conflict resolver:");
        PrintDictionary(mergedScores);

        Console.WriteLine();
        List<Product> productsForMaxBy = new List<Product>
        {
            new Product(10, "Keyboard", 80m),
            new Product(11, "Tablet", 700m),
            new Product(12, "Camera", 900m)
        };
        Product mostExpensiveProduct = CollectionUtils.MaxBy(productsForMaxBy, product => product.Price);
        Console.WriteLine("Most expensive product:");
        Console.WriteLine(mostExpensiveProduct);
    }

    private static void PrintNullable<T>(T? item)
    {
        if (item == null)
        {
            Console.WriteLine("Not found");
        }
        else
        {
            Console.WriteLine(item);
        }
    }

    private static void PrintList<T>(IReadOnlyList<T> items)
    {
        foreach (T item in items)
        {
            Console.WriteLine(item);
        }
    }

    private static void PrintDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        foreach (KeyValuePair<TKey, TValue> pair in dictionary)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }

    private static void PrintDictionaryOfLists<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
    {
        foreach (KeyValuePair<TKey, List<TValue>> pair in dictionary)
        {
            Console.Write($"{pair.Key}: ");

            for (int i = 0; i < pair.Value.Count; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }

                Console.Write(pair.Value[i]);
            }

            Console.WriteLine();
        }
    }
}
