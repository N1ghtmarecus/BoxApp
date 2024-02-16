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
              $"\n| {"ID",  -4} | {"Cut nr",-6} | {"Fefco",-5} |  {"Length", -7} |  {"Width" ,-6} |  {"Height", -7} |\n" +
                $"| {"----",-4} | {"------",-6} | {"-----",-5} | {"--------",-7} | {"-------",-6} | {"--------",-7} |\n" +
                $"| {Id, -4:D3} | {CutterNr,-7:D3}| {Fefco, -5} |  {Length,  -6}  |  {Width,  -5}  |  {Height,  -6}  | ";
        }

        return $"| {Id,-4:D3} | {CutterNr,-7:D3}| {Fefco,-5} |  {Length,-6}  |  {Width,-5}  |  {Height,-6}  | ";
    }
}