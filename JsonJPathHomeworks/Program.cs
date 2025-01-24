using Newtonsoft.Json.Linq;

namespace JsonJPathHomeworks
{
    public class Program
    {
        static void Main(string[] args)
        {
            string json = @"
        
{
    'departments': [
        {
            'name': 'Engineering',
            'employees': [
                { 'name': 'Alice', 'age': 30, 'skills': ['C#', 'SQL'] },
                { 'name': 'Bob', 'age': 35, 'skills': ['Java', 'AWS'] }
            ]
        },
        {
            'name': 'HR',
            'employees': [
                { 'name': 'Charlie', 'age': 28, 'skills': ['Recruitment', 'Communication'] },
                { 'name': 'Diana', 'age': 32, 'skills': ['Onboarding', 'Training'] }
            ]
        }
    ]
}";

            JObject data = JObject.Parse(json);

            var AllEmployees = data.SelectTokens("$.departments[?(@.name == 'Engineering')].employees[*].name").Select(employee => employee.ToString()).ToList();

            foreach (var employee in AllEmployees)
            {
                Console.WriteLine(employee);

            }

            var allUniqueSkills = data.SelectTokens("$.departments[*].employees[*].skills[*]").Select(token => token.ToString()).Distinct().ToList(); 
                    
            foreach (var skill in allUniqueSkills)
            {
                Console.WriteLine(skill);
            }

            var employeesOver30 = data.SelectTokens("$.departments[*]")
                          .SelectMany(dept => dept["employees"].Select(emp => new
                          {
                              Department = dept["name"].ToString(),
                              Name = emp["name"].ToString(),
                              Age = (int)emp["age"]
                          }))
                          .Where(emp => emp.Age > 30)
                          .ToList();

            foreach(var employee in employeesOver30)
            {
                Console.WriteLine(employee);
            }
        }
    }

}
