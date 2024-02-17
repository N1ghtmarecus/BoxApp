using BoxApp.Data.Repositories;
using BoxApp.Helpers;

namespace BoxApp.Data.Entities;

public class BoxCalculator(IRepository<Box> boxRepository, InputHelper inputHelper)
{
    private readonly IRepository<Box> _boxRepository = boxRepository;
    private readonly InputHelper _inputHelper = inputHelper;

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

    public void CalculateCardboardSizeForExistingBox(BoxCalculator calculator)
    {
        Console.Write("Enter the Box ID to calculate: ");
        do
        {
            if (!_inputHelper.TryGetBoxId(out var boxToCalculateId))
            {
                continue;
            }

            Box box = _boxRepository.GetById(boxToCalculateId!.Id)!;

            CalculateAndDisplayCardboardSize(calculator, box);
            break;
        } while (true);
    }

    public void CalculateCardboardSizeForNewBox(BoxCalculator calculator)
    {
        Console.WriteLine("\nEnter box dimensions:");

        int length = InputHelper.GetUserInputAsInt("Length: ", 10, 2000);
        int width = InputHelper.GetUserInputAsInt("Width: ", 10, 2000);
        int height = InputHelper.GetUserInputAsInt("Height: ", 10, 2000);

        Box box = new()
        {
            Length = length,
            Width = width,
            Height = height
        };

        CalculateAndDisplayCardboardSize(calculator, box);
    }

    private static void CalculateAndDisplayCardboardSize(BoxCalculator calculator, Box box)
    {
        float cardboardLength = CalculateCardboardLength(box);
        float cardboardWidth = CalculateCardboardWidth(box);
        float cardboardArea = cardboardLength * cardboardWidth / 1000000;

        Console.WriteLine($"\nFor such a box, the cardboard dimensions are:\n" +
                          $"Length = {cardboardLength}mm,\n" +
                          $"Width = {cardboardWidth}mm.\n" +
                          $"So the cardboard area is: {cardboardArea:N3}m²");
    }
}