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
        ChooseOptions();

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
                        ChooseOptions();
                        break;

                    case "2":
                        Menu();
                        AddNewBox();
                        _boxRepository.Save();
                        ChooseOptions();
                        break;

                    case "3":
                        EditBox();
                        _boxRepository.Save();
                        ChooseOptions();
                        break;

                    case "4":
                        RemoveBox();
                        _boxRepository.Save();
                        ChooseOptions();
                        break;

                    case "5":
                        ClearDatabase();
                        _boxRepository.Save();
                        ChooseOptions();
                        break;

                    case "6":
                        _filterBoxesProvider.FilterBoxes();
                        _boxRepository.Save();
                        Menu();
                        ChooseOptions();
                        break;

                    case "7":
                        Console.WriteLine("\nQuiting the program. Goodbye!");
                        return;

                    default:
                        Console.Write("\nInvalid choice. Please try again: ");
                        continue;
                }
        }
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║      Welcome to Box Catalog    ║");
        Console.WriteLine("╠════════════════════════════════╣");
        Console.WriteLine("║ 1. Display all available boxes ║");
        Console.WriteLine("║ 2. Add a new box               ║");
        Console.WriteLine("║ 3. Edit an existing box        ║");
        Console.WriteLine("║ 4. Remove an existing box      ║");
        Console.WriteLine("║ 5. Clear the database          ║");
        Console.WriteLine("║ 6. More information            ║");
        Console.WriteLine("║ 7. Quit                        ║");
        Console.WriteLine("╚════════════════════════════════╝");
    }

    static void ChooseOptions()
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
        int cutterNr;
        do
        {
            cutterNr = GetUserInputAsInt("Enter cutter number: ", 1, 999);

            if (_boxRepository.GetAll().Any(box => box.CutterNr == cutterNr))
            {
                Console.WriteLine($"Box with cutter number {cutterNr} already exists. Please enter a different cutter number.");
            }
        } while (_boxRepository.GetAll().Any(box => box.CutterNr == cutterNr));

        int fefco = GetUserInputAsInt("Enter Fefco: ", 1, 999);
        int length = GetUserInputAsInt("Enter Length: ", 1, 2000);
        int width = GetUserInputAsInt("Enter Width: ", 1, 2000);
        int height = GetUserInputAsInt("Enter Height: ", 1, 2000);

        var newBox = new Box
        {
            CutterNr = cutterNr,
            Fefco = fefco,
            Length = length,
            Width = width,
            Height = height,
        };

        _boxRepository.Add(newBox);
    }

    private static int GetUserInputAsInt(string prompt, int minValue, int maxValue)
    {
        int result;

        do
        {
            Console.Write(prompt);
            string userInput = Console.ReadLine()!;

            if (!int.TryParse(userInput, out result) || result < minValue || result > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a valid number in the range {minValue}-{maxValue}.");
            }

        } while (result < minValue || result > maxValue);

        return result;
    }

    private void RemoveBox()
    {
        Console.Write("\nEnter the ID of the box to remove: ");
        if (int.TryParse(Console.ReadLine(), out int boxId))
        {
            var boxToRemove = _boxRepository.GetById(boxId);

            if (boxToRemove != null)
                _boxRepository.Remove(boxToRemove);
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a valid ID.");
        }
    }

    private void EditBox()
    {   
        
        Console.Write("Enter the ID of the box to edit: ");
        do
        {
            if (int.TryParse(Console.ReadLine(), out int boxId))
            {
                var boxToEdit = _boxRepository.GetById(boxId);

                if (boxToEdit != null)
                {
                    Console.WriteLine("\nCurrent Box Information:");
                    Console.WriteLine(boxToEdit);
                    Box.ColumnNamesDisplayed = false;

                    Console.WriteLine("\nEnter new information:");

                    int cutterNr = GetUserInputAsInt("Enter cutter number: ", 1, 999);
                    int fefco = GetUserInputAsInt("Enter Fefco: ", 1, 999);
                    int length = GetUserInputAsInt("Enter Length: ", 1, 2000);
                    int width = GetUserInputAsInt("Enter Width: ", 1, 2000);
                    int height = GetUserInputAsInt("Enter Height: ", 1, 2000);

                    boxToEdit.Fefco = fefco;
                    boxToEdit.CutterNr = cutterNr;
                    boxToEdit.Length = length;
                    boxToEdit.Width = width;
                    boxToEdit.Height = height;

                    _boxRepository.Edit(boxToEdit);
                    break;
                }
                else
                {
                    Console.Write($"\nBox with ID '{boxId}' not found. Please enter a valid ID: ");
                }
            }
            else
            {
                Console.Write("\nInvalid input. Please enter a valid ID: ");
            }
        } while (true);
    }

    private void ClearDatabase()
    {
        Console.Write("\nAre you sure you want to clear the database? (Y/N): ");
        var userInput = Console.ReadLine();
        if (userInput?.ToUpper() == "Y")
        {
            try
            {
                _boxRepository.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException catched: {e.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nDatabase clearing aborted.");
        }
    }
}