using BoxApp.Entities;
using BoxApp.Repositories;

class Program
{
    static void Main()
    {
        Console.WriteLine("-------------------------------");
        Console.WriteLine("  Welcome to the Box Catalog!  ");
        Console.WriteLine("-------------------------------");

        var boxRepository = new JsonRepository<Box>();
        boxRepository.ItemAdded += BoxRepositoryOnItemAdded;
        boxRepository.ItemRemoved += BoxRepositoryOnItemRemoved;
        boxRepository.DbCleared += BoxRepositoryOnDbCleared;

        while (true)
        {
            Menu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Display(boxRepository);
                    break;

                case "2":
                    AddNewBox(boxRepository);
                    boxRepository.Save();
                    break;

                case "3":
                    RemoveBox(boxRepository);
                    boxRepository.Save();
                    break;

                case "4":
                    ClearDatabase(boxRepository);
                    boxRepository.Save();
                    break;

                case "5":
                    Console.WriteLine("\nExiting the program. Goodbye!");
                    return;

                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    break;
            }
        }
    }

    static void Menu()
    {
        Console.WriteLine("\nMENU:");
        Console.WriteLine("1. Display all available boxes");
        Console.WriteLine("2. Add a new box");
        Console.WriteLine("3. Remove an existing box");
        Console.WriteLine("4. Clear the database");
        Console.WriteLine("5. Exit the program");
        Console.Write("Choose an option (1-5): ");
    }

    static void BoxRepositoryOnItemAdded(object? sender, Box b)
    {
        Console.WriteLine($"\nBox added successfully => {b.Length}x{b.Width}x{b.Height} from {sender?.GetType().Name}");   
    }

    static void BoxRepositoryOnItemRemoved(object? sender, Box b)
    {
        Console.WriteLine($"\nBox with ID {b.Id} removed successfully. => {b.Length}x{b.Width}x{b.Height} from {sender?.GetType().Name}");
    }

    static void BoxRepositoryOnDbCleared(object? sender, Box b)
    {
        Console.WriteLine($"\nDatabase cleared successfully from {sender?.GetType().Name}");
    }

    static void Display(IReadRepository<IEntity> repository)
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
        Console.Write("Enter Fefco: ");
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
        Console.Write("\nAre you sure you want to clear the database? (y/n): ");
        var userInput = Console.ReadLine();
        if (userInput?.ToLower() == "y")
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