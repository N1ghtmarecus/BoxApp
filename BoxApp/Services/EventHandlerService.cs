using BoxApp.Data.Entities;
using BoxApp.Data.Repositories;

namespace BoxApp.Services;

public class EventHandlerService(IRepository<Box> boxRepository) : IEventHandlerService
{
    private readonly IRepository<Box> _boxRepository = boxRepository;

    public void SubscribeToEvents()
    {
        _boxRepository.ItemAdded += BoxRepositoryOnItemAdded;
        _boxRepository.ItemRemoved += BoxRepositoryOnItemRemoved;
        _boxRepository.ItemUpdated += BoxRepositoryOnItemUpdated;
        _boxRepository.DbCleared += BoxRepositoryOnDbCleared;
    }

    private void BoxRepositoryOnItemAdded(object? sender, Box b)
    {
        Console.WriteLine($"\nBox added successfully => '{b.Length}x{b.Width}x{b.Height}' to {sender?.GetType().Name}");
    }

    private void BoxRepositoryOnItemRemoved(object? sender, Box b)
    {
        Console.WriteLine($"\nBox with ID '{b.Id:D3}' removed successfully. => {b.Length}x{b.Width}x{b.Height} from {sender?.GetType().Name}");
    }

    private void BoxRepositoryOnItemUpdated(object? sender, Box b) 
    {
        Console.WriteLine($"\nBox with ID '{b.Id:D3}' updated successfully. => '{b.Length}x{b.Width}x{b.Height}' to {sender?.GetType().Name}");
    }

    private void BoxRepositoryOnDbCleared(object? sender, Box b)
    {
        Console.WriteLine($"\nDatabase '{sender?.GetType().Name}' cleared successfully.");
    }
}
