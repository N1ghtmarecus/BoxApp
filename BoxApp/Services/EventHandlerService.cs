using BoxApp.Entities;
using BoxApp.Repositories;

namespace BoxApp.Services;

public class EventHandlerService(IRepository<Box> boxRepository) : IEventHandlerService
{
    private readonly IRepository<Box> _boxRepository = boxRepository;

    public void SubscribeToEvents()
    {
        _boxRepository.ItemAdded += BoxRepositoryOnItemAdded;
        _boxRepository.ItemRemoved += BoxRepositoryOnItemRemoved;
        _boxRepository.DbCleared += BoxRepositoryOnDbCleared;
    }

    private void BoxRepositoryOnItemAdded(object? sender, Box b)
    {
        Console.WriteLine($"\nBox added successfully => {b.Length}x{b.Width}x{b.Height} to {sender?.GetType().Name}");
    }

    private void BoxRepositoryOnItemRemoved(object? sender, Box b)
    {
        Console.WriteLine($"\nBox with ID {b.Id} removed successfully. => {b.Length}x{b.Width}x{b.Height} from {sender?.GetType().Name}");
    }

    private void BoxRepositoryOnDbCleared(object? sender, Box b)
    {
        Console.WriteLine($"\nDatabase cleared successfully from {sender?.GetType().Name}");
    }
}
