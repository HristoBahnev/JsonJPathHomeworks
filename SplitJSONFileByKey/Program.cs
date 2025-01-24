using Newtonsoft.Json.Linq;

namespace SplitJSONFileByKey
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = JArray.Parse(File.ReadAllText("C:\\QA Automation - Blankfactor\\JsonJPathHomeworks\\JsonJPathHomeworks\\transactions.json"));

            var credits = transactions.Where(t => (string)t["type"] == "credit").ToList();
            var debits = transactions.Where(t => (string)t["type"] == "debit").ToList();

            File.WriteAllText("credits.json", new JArray(credits).ToString());
            File.WriteAllText("debits.json", new JArray(debits).ToString());

            Console.WriteLine($"Total Credit Amount: {credits.Sum(t => (decimal)t["amount"])}");
            Console.WriteLine($"Total Debit Amount: {debits.Sum(t => (decimal)t["amount"])}");
        }
    }
}
