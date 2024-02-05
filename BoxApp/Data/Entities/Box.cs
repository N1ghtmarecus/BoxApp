namespace BoxApp.Data.Entities;

public class Box : EntityBase
{
    public int CutterNr { get; set; }

    public int Fefco { get; set; }

    public int Length { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
    public static bool ColumnNamesDisplayed { get => columnNamesDisplayed; set => columnNamesDisplayed = value; }

    private static bool columnNamesDisplayed;

    public override string ToString()
    {
        if (!ColumnNamesDisplayed)
        {
            ColumnNamesDisplayed = true;
            return
              $"\n| {"ID",  -4} | {"Cutter number",-13} | {"Fefco",-5} |  {"Length", -7} |  {"Width" ,-6} |  {"Height", -7} |\n" +
                $"| {"----",-4} | {"-------------",-13} | {"-----",-5} | {"--------",-7} | {"-------",-6} | {"--------",-7} |\n" +
                $"| {Id, -4:D3} |      {CutterNr,-8:D3} |  {Fefco, -4} |  {Length,  -6}  |  {Width,  -5}  |  {Height,  -6}  | ";
        }

        return $"| {Id,-4:D3} |      {CutterNr,-8:D3} |  {Fefco,-4} |  {Length,-6}  |  {Width,-5}  |  {Height,-6}  | ";
    }
}