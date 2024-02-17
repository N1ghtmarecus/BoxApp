using BoxApp.Data.Entities;
using BoxApp.Data.Repositories;
using BoxApp.Helpers;

namespace BoxApp.Services;

public class UserCommunication(IRepository<Box> boxRepository, IFilterBoxesProvider filterBoxesProvider, InputHelper inputHelper, BoxCalculator boxCalculator) : IUserCommunication
{
    private readonly IRepository<Box> _boxRepository = boxRepository;
    private readonly IFilterBoxesProvider _filterBoxesProvider = filterBoxesProvider;
    private readonly InputHelper _inputHelper = inputHelper;
    private readonly BoxCalculator _boxCalculator = boxCalculator;

    public void UserChoice()
    {
        MainMenu();
        ChooseOptionHelper.ChooseOption(1, 8);

        while (true)
        {
            Box.ColumnNamesDisplayed = false;
            var userChoice = Console.ReadLine();

            if (userChoice != null)
                ProcessUserChoice(userChoice);
        }
    }

    private void ProcessUserChoice(string userChoice)
    {
        switch (userChoice.ToUpper())
        {
            case "1":
                HandleOption1();
                break;

            case "2":
                HandleOption2();
                break;

            case "3":
                HandleOption3();
                break;

            case "4":
                HandleOption4();
                break;

            case "5":
                HandleOption5();
                break;

            case "6":
                HandleOption6();
                break;

            case "7":
                HandleOption7();
                break;

            case "8":
                Console.WriteLine("\nQuitting the program. Goodbye!");
                Environment.Exit(0);
                return;

            default:
                Console.Write($"\nInvalid choice '{userChoice}'. Please enter a valid number in the range 1-8: ");
                break;
        }
    }

    private void HandleOption1()
    {
        MainMenu();
        DisplayAll();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private void HandleOption2()
    {
        MainMenu();
        AddNewBox();
        _boxRepository.Save();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private void HandleOption3()
    {
        EditBox();
        _boxRepository.Save();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private void HandleOption4()
    {
        RemoveBox();
        _boxRepository.Save();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private void HandleOption5()
    {
        ClearDatabase();
        _boxRepository.Save();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private void HandleOption6()
    {
        _filterBoxesProvider.FilterBoxes();
        _boxRepository.Save();
        MainMenu();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private void HandleOption7()
    {
        CalculateCardboardSize();
        MainMenu();
        ChooseOptionHelper.ChooseOption(1, 8);
    }

    private static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════╗\n" +
                          "║     Welcome to Box Catalog      ║\n" +
                          "╠═════════════════════════════════╣\n" +
                          "║ 1. Display all available boxes  ║\n" +
                          "║ 2. Add a new box                ║\n" +
                          "║ 3. Edit an existing box         ║\n" +
                          "║ 4. Remove an existing box       ║\n" +
                          "║ 5. Clear the database           ║\n" +
                          "║ 6. Boxes filters                ║\n" +
                          "║ 7. Calculate the cardboard size ║\n" +
                          "║ 8. Quit                         ║\n" +
                          "╚═════════════════════════════════╝");
    }

    private static void CalculateCardboardSizeMenu()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════╗\n" +
                          "║     Calculate Cardboard Size    ║\n" +
                          "╠═════════════════════════════════╣\n" +
                          "║ 1. From an existing box         ║\n" +
                          "║ 2. For a new box                ║\n" +
                          "║ 3. Back to MAIN MENU            ║\n" +
                          "╚═════════════════════════════════╝");
    }

    public void CalculateCardboardSize()
    {
        CalculateCardboardSizeMenu();

        ChooseOptionHelper.ChooseOption(1, 3);

        while (true)
        {
            var userChoice = Console.ReadLine();

            if (userChoice != null)
                switch (userChoice?.ToUpper())
                {
                    case "1":
                        _boxCalculator.CalculateCardboardSizeForExistingBox(_boxCalculator);
                        ChooseOptionHelper.ChooseOption(1, 3);
                        break;
                    case "2":
                        _boxCalculator.CalculateCardboardSizeForNewBox(_boxCalculator);
                        ChooseOptionHelper.ChooseOption(1, 3);
                        break;
                    case "3":
                        return;
                    default:
                        Console.Write($"\nInvalid choice '{userChoice}'. Please enter a valid number in the range 1-3: ");
                        continue;
                }
        }
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
        _inputHelper.GetUniqueBoxInputs(newBox);

        try
        {
            _boxRepository.Add(newBox);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred while adding the box: {ex.Message}");
        }
    }

    private void RemoveBox()
    {
        Console.Write("\nEnter the Box ID to remove: ");
        do
        {
            if (!_inputHelper.TryGetBoxId(out var boxToRemove))
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
            if (!_inputHelper.TryGetBoxId(out var boxToEdit))
            {
                continue;
            }

            Console.WriteLine("\nCurrent Box Information:");
            Console.WriteLine(boxToEdit);
            Box.ColumnNamesDisplayed = false;

            Console.WriteLine("\nEnter new informations:");
            _inputHelper.GetUniqueBoxInputs(boxToEdit!, true, boxToEdit!.CutterNr);

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