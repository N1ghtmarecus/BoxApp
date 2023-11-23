namespace BoxApp.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }

        public int Fefco { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string? Flute { get; set; }

        public int Grammage { get; set; }
    }
}
