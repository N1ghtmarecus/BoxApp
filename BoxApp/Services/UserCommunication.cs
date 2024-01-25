using BoxApp.Entities;
using BoxApp.Repositories;

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
                    DisplayAll(_boxRepository);
                    ChooseOptions();
                    break;

                case "2":
                    Menu();
                    AddNewBox(_boxRepository);
                    _boxRepository.Save();
                    ChooseOptions();
                    break;

                case "3":
                    RemoveBox(_boxRepository);
                    _boxRepository.Save();
                    ChooseOptions();
                    break;

                case "4":
                    ClearDatabase(_boxRepository);
                    _boxRepository.Save();
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
        Console.WriteLine(
                "---------------------------------\n" +
                "|  Welcome to the Box Catalog!  |\n" +
                "---------------------------------\n" +
                "\n             MENU\n" +
                "1. Display all available boxes\n" +
                "2. Add a new box\n" +
                "3. Remove an existing box\n" +
                "4. Clear the database\n" +
                "5. More informations\n" +
                "6. Quit the program");
    }

    static void ChooseOptions()
    {
        Console.Write("\nChoose an option (1-6): ");
    }

    static void DisplayAll(IReadRepository<IEntity> repository)
    {
        Console.WriteLine("\n----- BOX CATALOG -----");
        var items = repository.GetAll();
        if (items.ToList().Count == 0)
        {
            Console.WriteLine("\nNo objects found!");
        }
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    static void AddNewBox(IRepository<Box> boxRepository)
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

        boxRepository.Add(newBox);
    }

    static void RemoveBox(IRepository<Box> boxRepository)
    {
        Console.Write("\nEnter the ID of the box to remove: ");
        if (int.TryParse(Console.ReadLine(), out int boxId))
        {
            var boxToRemove = boxRepository.GetById(boxId);

            if (boxToRemove != null)
                boxRepository.Remove(boxToRemove);
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a valid ID.");
        }
    }

    static void ClearDatabase(IRepository<Box> boxRepository)
    {
        Console.Write("\nAre you sure you want to clear the database? (Y/N): ");
        var userInput = Console.ReadLine();
        if (userInput?.ToUpper() == "Y")
        {
            try
            {
                boxRepository.Clear();
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
