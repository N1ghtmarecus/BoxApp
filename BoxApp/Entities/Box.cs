namespace BoxApp.Entities;

public class Box : EntityBase
{
    public override string ToString() =>
        $"ID: {Id,-3} | " +
        $"Fefco: {Fefco,-3} | " +
        $"Length: {Length,-4} | " +
        $"Width: {Width,-4} | " +
        $"Height: {Height,-4} | " +
        $"Flute: {Flute,-2} | " +
        $"Grammage: {Grammage,-4}g/m2";
}