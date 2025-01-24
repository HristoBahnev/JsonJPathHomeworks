using Newtonsoft.Json;
using System.Xml;

namespace ComplexObjectSerializationAndDeserialization
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Anthony",
                Employer = new Company
                {
                    Name = "Manchester United",
                    Location = new Address { Street = "Old Trafford M16", City = "Manchester" }
                },
                Skills = new List<string> { "Sitting on the bench", "Drinking water" }
            },
            new Employee
            {
                Id = 2,
                Name = "Bai Ivan",
                Employer = new Company
                {
                    Name = "Hapka Piika EOOD",
                    Location = new Address { Street = "Ivan Vazov 1", City = "Sofia" }
                },
                Skills = new List<string> { "Eating", "Drinking" }
            }
        };

            string serializedEmployees = JsonConvert.SerializeObject(employees);
            Console.WriteLine("\nSerialized Employees:");
            Console.WriteLine(serializedEmployees);

            var deserializedEmployees = JsonConvert.DeserializeObject<List<Employee>>(serializedEmployees);

            Console.WriteLine("\nDeserialized Employees:");
            foreach (var emp in deserializedEmployees)
            {
                Console.WriteLine($"Name: {emp.Name}, Company: {emp.Employer.Name}, Address: {emp.Employer.Location.Street}, {emp.Employer.Location.City}");
                Console.WriteLine("Skills: " + string.Join(", ", emp.Skills));
            }

            var employeesInCity = deserializedEmployees.Where(e => e.Employer.Location.City == "Sofia").Select(e => e.Name).ToList();
            Console.WriteLine("\nEmployees in Sofia:");
            foreach (var name in employeesInCity)
            {
                Console.WriteLine(name);
            }
        }
    }
}