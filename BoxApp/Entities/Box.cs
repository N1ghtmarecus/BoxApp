namespace BoxApp.Entities;

public class Box : EntityBase
{
    public int Fefco { get; set; }

    public int Length { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string? Flute { get; set; }

    public int Grammage { get; set; }

    public override string ToString() =>
        $"ID: {Id,-3} | " +
        $"Fefco: {Fefco,-3} | " +
        $"Length: {Length,-4} | " +
        $"Width: {Width,-4} | " +
        $"Height: {Height,-4} | " +
        $"Flute: {Flute,-2} | " +
        $"Grammage: {Grammage,-4}g/m2";
}