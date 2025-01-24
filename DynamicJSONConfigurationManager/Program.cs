using Newtonsoft.Json.Linq;
class Program
{
    const string FilePath = "C:\\QA Automation - Blankfactor\\JsonJPathHomeworks\\JsonJPathHomeworks\\config.json";

    static void Main()
    {
        var config = JObject.Parse(File.ReadAllText(FilePath));

        Console.WriteLine("Current Configuration:");
        Console.WriteLine(config.ToString());

        UpdateFeatureFlag(config, "darkMode", false);

        PrintSecureApis(config);

        AddApiEndpoint(config, new JObject
        {
            ["name"] = "OrderService",
            ["url"] = "https://api.example.com/orders",
            ["isSecure"] = true
        });

        File.WriteAllText(FilePath, config.ToString());
    }

    static void UpdateFeatureFlag(JObject config, string featureName, bool isEnabled)
    {
        config["featureFlags"][featureName] = isEnabled;
        Console.WriteLine($"Feature '{featureName}' set to {isEnabled}.");
    }

    static void PrintSecureApis(JObject config)
    {
        var secureApis = config["apiEndpoints"]
            .Where(api => (bool)api["isSecure"])
            .Select(api => api["url"].ToString());

        Console.WriteLine("Secure APIs:");
        foreach (var api in secureApis)
        {
            Console.WriteLine(api);
        }
    }

    static void AddApiEndpoint(JObject config, JObject newApi)
    {
        var apiEndpoints = (JArray)config["apiEndpoints"];
        if (apiEndpoints.Any(api => api["name"].ToString() == newApi["name"].ToString()))
        {
            Console.WriteLine($"API with name '{newApi["name"]}' already exists.");
            return;
        }

        apiEndpoints.Add(newApi);
        Console.WriteLine($"API '{newApi["name"]}' added successfully.");
    }
}
