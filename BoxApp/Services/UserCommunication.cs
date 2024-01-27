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
            var choice = Console.ReadLine();

            switch (choice?.ToUpper())
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
                    RemoveBox();
                    _boxRepository.Save();
                    ChooseOptions();
                    break;

                case "4":
                    ClearDatabase();
                    _boxRepository.Save();
                    ChooseOptions();
                    break;

                case "5":
                    _filterBoxesProvider.FilterBoxes();
                    _boxRepository.Save();
                    Menu();
                    ChooseOptions();
                    break;

                case "6":
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
        Console.WriteLine("║ 3. Remove an existing box      ║");
        Console.WriteLine("║ 4. Clear the database          ║");
        Console.WriteLine("║ 5. More information            ║");
        Console.WriteLine("║ 6. Quit                        ║");
        Console.WriteLine("╚════════════════════════════════╝");
    }

    static void ChooseOptions()
    {
        Console.Write("\nChoose an option (1-6): ");
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
        Console.Write("\nEnter Fefco: ");
        int fefco;
        while (!int.TryParse(Console.ReadLine(), out fefco))
        {
            Console.WriteLine("Invalid Fefco. Please enter a valid number.");
            Console.Write("Enter Fefco: ");
        }

        Console.Write("Enter Length: ");
        int length;
        while (!int.TryParse(Console.ReadLine(), out length))
        {
            Console.WriteLine("Invalid Length. Please enter a valid number.");
            Console.Write("Enter Length: ");
        }

        Console.Write("Enter Width: ");
        int width;
        while (!int.TryParse(Console.ReadLine(), out width))
        {
            Console.WriteLine("Invalid Width. Please enter a valid number.");
            Console.Write("Enter Width: ");
        }

        Console.Write("Enter Height: ");
        int height;
        while (!int.TryParse(Console.ReadLine(), out height))
        {
            Console.WriteLine("Invalid Height. Please enter a valid number.");
            Console.Write("Enter Height: ");
        }

        Console.Write("Enter Flute: ");
        string? flute = Console.ReadLine();

        while (string.IsNullOrEmpty(flute))
        {
            Console.WriteLine("Invalid Flute. Please enter a valid value.");
            Console.Write("Enter Flute: ");
            flute = Console.ReadLine();
        }

        Console.Write("Enter Grammage: ");
        int grammage;
        while (!int.TryParse(Console.ReadLine(), out grammage))
        {
            Console.WriteLine("Invalid Grammage. Please enter a valid number.");
            Console.Write("Enter Grammage: ");
        }

        var newBox = new Box { Fefco = fefco, Length = length, Width = width, Height = height, Flute = flute, Grammage = grammage };

        _boxRepository.Add(newBox);
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
