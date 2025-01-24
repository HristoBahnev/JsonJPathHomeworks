namespace ComplexObjectSerializationAndDeserialization
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Employer { get; set; }
        public List<string> Skills { get; set; }
    }
}
