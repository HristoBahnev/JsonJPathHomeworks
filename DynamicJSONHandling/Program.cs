using Newtonsoft.Json.Linq;

namespace DynamicJSONHandling
{
   public class Program
    {
        static void Main(string[] args)
        {
            string json = @"{
                'store': {
                    'products': [
                            { 'id': 1, 'name': 'Laptop', 'price': 1200, 'category': 'Electronics', 'stock': 10 },
                            { 'id': 2, 'name': 'Tablet', 'price': 800, 'category': 'Electronics', 'stock': 0 },
                            { 'id': 3, 'name': 'Notebook', 'price': 15, 'category': 'Stationery', 'stock': 50 },
                            { 'id': 4, 'name': 'Pen', 'price': 2, 'category': 'Stationery', 'stock': 100 }
                                ],
                    'lastUpdated': '2025-01-01T10:00:00Z'
                 }
            }";

            JObject data = JObject.Parse(json);

            var newProduct = new JObject
            {
                ["id"] = 5,
                ["name"] = "Headphones",
                ["price"] = 150,
                ["category"] = "Electronics",
                ["stock"] = 25
            };
            data["store"]["products"].Last.AddAfterSelf(newProduct);

            foreach (var product in data["store"]["products"].Where(p => p["category"].ToString() == "Electronics"))
            {
                if ((int)product["stock"] == 0)
                {
                    product["stock"] = 50;
                }
            }

            int totalStock = data["store"]["products"].Sum(p => (int)p["stock"]);
            data["store"]["totalStock"] = totalStock;


            var products = data["store"]["products"];
            var filteredProducts = products.Where(p => (int)p["price"] >= 10).ToList();
            data["store"]["products"] = new JArray(filteredProducts);


            string modifiedJson = data.ToString();
            Console.WriteLine("Modified JSON:");
            Console.WriteLine(modifiedJson);

        }
    }
}
