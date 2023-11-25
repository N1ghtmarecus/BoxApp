using BoxApp.Data;
using BoxApp.Entities;
using BoxApp.Repositories;
using BoxApp.Repositories.Extensions;

var fefcoRepository = new SqlRepository<Box>(new BoxAppDbContext(), BoxAdded);
fefcoRepository.ItemAdded += FefcoRepositoryOnItemAdded;

static void FefcoRepositoryOnItemAdded(object? sender, Box b)
{
    Console.WriteLine($"Box added => {b.Fefco} from {sender?.GetType().Name}");
}

AddFefcoBoxes(fefcoRepository);
Display(fefcoRepository);

static void BoxAdded(Box item)
{
    Console.WriteLine($"Fefco{item.Fefco} {item.Length}x{item.Width}x{item.Height} added");
}

static void AddFefcoBoxes(IRepository<Box> fefcoRepository)
{
    var boxes = new[]
    {
        new Box { Fefco = 201, Length = 80, Width = 70, Height = 40, Flute = "E", Grammage = 360 },
        new Box { Fefco = 201, Length = 90, Width = 60, Height = 80, Flute = "B", Grammage = 420 },
        new Box { Fefco = 201, Length = 205, Width = 115, Height = 60, Flute = "EB", Grammage = 650 },
    };

    fefcoRepository.AddBatch(boxes);
}

static void Display(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
        Console.WriteLine(item);
}