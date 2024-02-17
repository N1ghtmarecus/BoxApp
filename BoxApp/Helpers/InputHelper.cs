using BoxApp.Data.Entities;
using BoxApp.Data.Repositories;

namespace BoxApp.Helpers;

public class InputHelper(IRepository<Box> boxRepository)
{
    private readonly IRepository<Box> _boxRepository = boxRepository;
    public static int GetUserInputAsInt(string prompt, int minValue, int maxValue)
    {
        int result;
        do
        {
            Console.Write(prompt);
            string? userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out result) || result < minValue || result > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a valid number in the range {minValue}-{maxValue}.");
            }
        } while (result < minValue || result > maxValue);
        return result;
    }

    public bool TryGetBoxId(out Box? box)
    {
        var intCheck = Console.ReadLine();
        if (!int.TryParse(intCheck, out int boxId))
        {
            Console.Write($"\nInvalid input '{intCheck}'. Please enter a valid integer value: ");
            box = null;
            return false;
        }

        box = _boxRepository.GetById(boxId);
        if (box == null)
        {
            Console.Write($"\nThere is no box with ID '{boxId}'. Please enter a valid ID: ");
            return false;
        }
        return true;
    }

    public void GetUniqueBoxInputs(Box box, bool checkUniqueness = true, int? currentCutterNr = null)
    {
        int localCutterNr;
        do
        {
            localCutterNr = GetUserInputAsInt("Cutter number: ", 1, 999);

            if (checkUniqueness && localCutterNr != currentCutterNr && _boxRepository.GetAll().Any(box => box.CutterNr == localCutterNr))
            {
                Console.Write($"Box with cutter number '{localCutterNr:D3}' already exists in database. Please enter a different cutter number.\n");
            }
        } while (checkUniqueness && _boxRepository.GetAll().Any(box => box.CutterNr == localCutterNr && box.CutterNr != currentCutterNr));

        box.CutterNr = localCutterNr;
        box.Fefco = GetUserInputAsInt("Fefco: ", 200, 512);
        box.Length = GetUserInputAsInt("Length: ", 10, 2000);
        box.Width = GetUserInputAsInt("Width: ", 10, 2000);
        box.Height = GetUserInputAsInt("Height: ", 10, 2000);
    }
}