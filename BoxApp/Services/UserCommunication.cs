using BoxApp.Data.Entities;
using BoxApp.Data.Repositories;

namespace BoxApp.Services;

public class UserCommunication(IRepository<Box> boxRepository, IFilterBoxesProvider filterBoxesProvider) : IUserCommunication
{
    private readonly IRepository<Box> _boxRepository = boxRepository;
    private readonly IFilterBoxesProvider _filterBoxesProvider = filterBoxesProvider;

    public void UserChoice()
    {
        Menu();
        ChooseOption();

        while (true)
        {
            Box.ColumnNamesDisplayed = false;
            var choice = Console.ReadLine();

            if (choice != null)
                switch (choice.ToUpper())
                {
                    case "1":
                        Menu();
                        DisplayAll();
                        ChooseOption();
                        break;

                    case "2":
                        Menu();
                        AddNewBox();
                        _boxRepository.Save();
                        ChooseOption();
                        break;

                    case "3":
                        EditBox();
                        _boxRepository.Save();
                        ChooseOption();
                        break;

                    case "4":
                        RemoveBox();
                        _boxRepository.Save();
                        ChooseOption();
                        break;

                    case "5":
                        ClearDatabase();
                        _boxRepository.Save();
                        ChooseOption();
                        break;

                    case "6":
                        _filterBoxesProvider.FilterBoxes();
                        _boxRepository.Save();
                        Menu();
                        ChooseOption();
                        break;

                    case "7":
                        Console.WriteLine("\nQuiting the program. Goodbye!");
                        return;

                    default:
                        Console.Write($"\nInvalid choice '{choice}'. Please enter a valid number in the range 1-7: ");
                        continue;
                }
        }
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗\n" +
                          "║     Welcome to Box Catalog     ║\n" +
                          "╠════════════════════════════════╣\n" +
                          "║ 1. Display all available boxes ║\n" +
                          "║ 2. Add a new box               ║\n" +
                          "║ 3. Edit an existing box        ║\n" +
                          "║ 4. Remove an existing box      ║\n" +
                          "║ 5. Clear the database          ║\n" +
                          "║ 6. More information            ║\n" +
                          "║ 7. Quit                        ║\n" +
                          "╚════════════════════════════════╝");
    }

    static void ChooseOption()
    {
        Console.Write("\nChoose an option (1-7): ");
    }

    private void DisplayAll()
    {
        var items = _boxRepository.GetAll();
        if (items.ToList().Count == 0)
        {
            Console.WriteLine("\nNo objects found!");
        }
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    private void AddNewBox()
    {
        Console.WriteLine("\nEnter informations:");

        var newBox = new Box();
        GetUniqueBoxInputs(newBox);

        try
        {
            _boxRepository.Add(newBox);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"\nAn error occurred while adding the box: {ex.Message}");
        }
    }

    private void GetUniqueBoxInputs(Box box, bool checkUniqueness = true, int? currentCutterNr = null)
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
        box.Fefco = GetUserInputAsInt("Fefco: ", 1, 999);
        box.Length = GetUserInputAsInt("Length: ", 1, 2000);
        box.Width = GetUserInputAsInt("Width: ", 1, 2000);
        box.Height = GetUserInputAsInt("Height: ", 1, 2000);
    }

    private static int GetUserInputAsInt(string prompt, int minValue, int maxValue)
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

    private void RemoveBox()
    {
        Console.Write("\nEnter the Box ID to remove: ");
        do
        {
            if (!TryGetBoxId(out var boxToRemove))
            {
                continue;
            }

            Console.Write("\nAre you sure you want to remove this box from database? Y/N: ");
            var userInput = Console.ReadLine();
            if (userInput?.ToUpper() != "Y")
            {
                Console.WriteLine("\nBox removing aborted.");
                return;
            }

            try
            {
                _boxRepository.Remove(boxToRemove!);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred while removing the box: {ex.Message}");
            }
            break;
        } while (true);
    }

    private void EditBox()
    {
        Console.Write("Enter the Box ID to edit: ");
        do
        {
            if (!TryGetBoxId(out var boxToEdit))
            {
                continue;
            }

            Console.WriteLine("\nCurrent Box Information:");
            Console.WriteLine(boxToEdit);
            Box.ColumnNamesDisplayed = false;

            Console.WriteLine("\nEnter new informations:");
            GetUniqueBoxInputs(boxToEdit!, true, boxToEdit!.CutterNr);

            try
            {
                _boxRepository.Edit(boxToEdit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred while editing the box: {ex.Message}");
            }
            break;
        } while (true);
    }

    private bool TryGetBoxId(out Box? box)
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

    private void ClearDatabase()
    {
        Console.Write("\nAre you sure you want to clear the database? (Y/N): ");
        var userInput = Console.ReadLine();
        if (userInput?.ToUpper() != "Y")
        {
            Console.WriteLine("\nDatabase clearing aborted.");
        }

        try
        {
            _boxRepository.Clear();
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nAn error occurred while clearing the database: {e.Message}");
        }
    }
}