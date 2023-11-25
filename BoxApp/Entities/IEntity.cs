namespace BoxApp.Entities;

public interface IEntity
{
    int Id { get; set; }

    int Fefco { get; set; }

    int Length { get; set; }

    int Width { get; set; }

    int Height { get; set; }

    string? Flute { get; set; }

    int Grammage { get; set; }
}