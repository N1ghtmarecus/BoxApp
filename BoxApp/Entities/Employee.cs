namespace BoxApp.Entities
{
    public class Employee : EntityBase
    {
        public string? FirstName { get; set; }

        public override string ToString() => $"ID: {Id}, FirstName: {FirstName}";
    }
}
