namespace BoxApp.Data.Entities;

public class BoxCalculator
{
    public static float CalculateCardboardLength(Box box)
    {
        int cardboardLength = 2*box.Length + 2*box.Width + 30;

        return cardboardLength;
    }

    public static float CalculateCardboardWidth(Box box)
    {
        int cardboardWidth = box.Height + box.Width;

        return cardboardWidth;
    }
}
