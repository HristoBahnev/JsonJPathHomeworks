using Newtonsoft.Json.Linq;
using System.Text;

class Program
{
    const string FilePath = "C:\\QA Automation - Blankfactor\\JsonJPathHomeworks\\JsonJPathHomeworks\\inventory.json";

    static void Main(string[] args)
    {
        var inventory = LoadInventory();

        AddProduct(inventory, new JObject
        {
            ["productId"] = 103,
            ["productName"] = "Monitor",
            ["category"] = "Furniture",
            ["price"] = 150.00,
            ["stock"] = 20,
            ["sales"] = new JArray(3, 5, 2)
        });

        CalculateStockValue(inventory);

        FindBestSellingProduct(inventory);

        UpdateStock(inventory, 101, 25);

        SaveInventory(inventory);
    }

    static JArray LoadInventory()
    {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException("The inventory.json file does not exist.");

        var jsonData = File.ReadAllText(FilePath, Encoding.Default);
        return JArray.Parse(jsonData);
    }

    static void SaveInventory(JArray inventory)
    {
        File.WriteAllText(FilePath, inventory.ToString());
    }

    static void AddProduct(JArray inventory, JObject newProduct)
    {
        if (inventory.Any(p => (int)p["productId"] == (int)newProduct["productId"]))
        {
            Console.WriteLine($"Product with ID {newProduct["productId"]} already exists.");
            return;
        }

        inventory.Add(newProduct);
        Console.WriteLine($"Product {newProduct["productName"]} added successfully.");
    }

    static void CalculateStockValue(JArray inventory)
    {
        var stockValueByCategory = inventory
            .GroupBy(p => (string)p["category"])
            .ToDictionary(
                g => g.Key,
                g => g.Sum(p => (decimal)p["price"] * (int)p["stock"])
            );

        Console.WriteLine("Stock Value by Category:");
        foreach (var kvp in stockValueByCategory)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value:C}");
        }
    }

    static void FindBestSellingProduct(JArray inventory)
    {
        var bestProduct = inventory
            .Select(p => new
            {
                Name = (string)p["productName"],
                TotalSales = ((JArray)p["sales"]).Sum(s => (int)s)
            })
            .OrderByDescending(p => p.TotalSales)
            .First();

        Console.WriteLine($"Best-Selling Product: {bestProduct.Name} (Total Sales: {bestProduct.TotalSales})");
    }

    static void UpdateStock(JArray inventory, int productId, int newStock)
    {
        var product = inventory.FirstOrDefault(p => (int)p["productId"] == productId);
        if (product == null)
        {
            Console.WriteLine($"Product with ID {productId} not found.");
            return;
        }

        product["stock"] = newStock;
        Console.WriteLine($"Stock for product ID {productId} updated to {newStock}.");
    }
}
