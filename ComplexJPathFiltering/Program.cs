using Newtonsoft.Json.Linq;

public class Program
{
    static void Main(string[] args)
    {
        string json = @"
           {
    'orders': [
        {
            'orderId': 1,
            'customer': 'John Doe',
            'items': [
                { 'product': 'Laptop', 'price': 1200 },
                { 'product': 'Mouse', 'price': 25 }
            ]
        },
        {
            'orderId': 2,
            'customer': 'Jane Smith',
            'items': [
                { 'product': 'Phone', 'price': 800 },
                { 'product': 'Headphones', 'price': 100 }
            ]
        }
    ]
}";

        JObject data = JObject.Parse(json);

        var customerNames = data.SelectTokens("$.orders[*].customer").Select(c => c.ToString()).ToList();
        Console.WriteLine("Customers' Names:");
        customerNames.ForEach(name => Console.WriteLine(name));

        var expensiveProducts = data.SelectTokens("$.orders[*].items[?(@.price > 100)]")
                                    .Select(item => new
                                    {
                                        Product = item["product"].ToString(),
                                        Price = (int)item["price"]
                                    })
                                    .ToList();
        Console.WriteLine("\nProducts with Price > 100:");
        expensiveProducts.ForEach(p => Console.WriteLine($"Product: {p.Product}, Price: {p.Price}"));

        var totalFirstOrderPrice = data.SelectTokens("$.orders[?(@.orderId == 1)].items[*].price")
                                       .Sum(price => (int)price);
        Console.WriteLine($"\nTotal Price of Items in First Order: {totalFirstOrderPrice}");
    }
}
