using Newtonsoft.Json.Linq;

namespace MergedTwoJSONFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var products1 = JArray.Parse(File.ReadAllText("C:\\QA Automation - Blankfactor\\JsonJPathHomeworks\\JsonJPathHomeworks\\products1.json"));
            var products2 = JArray.Parse(File.ReadAllText("C:\\QA Automation - Blankfactor\\JsonJPathHomeworks\\JsonJPathHomeworks\\products2.json"));

            var mergedProducts = new JArray(products2);
            foreach (var product in products1)
            {
                if (!products2.Any(p => (int)p["id"] == (int)product["id"]))
                {
                    mergedProducts.Add(product);
                }
            }

            File.WriteAllText("mergedProducts.json", mergedProducts.ToString());
            Console.WriteLine("Merged products saved to mergedProducts.json.");
        }
    }
}
