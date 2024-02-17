namespace BoxApp.Helpers;

public class ChooseOptionHelper
{
    public static void ChooseOption(int minValue, int maxValue)
    {
        Console.Write($"\nChoose an option ({minValue}-{maxValue}): ");
    }
}
