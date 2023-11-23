using BoxApp.Data;
using BoxApp.Entities;
using BoxApp.Repositories;

var fefcoRepository = new SqlRepository<Box>(new BoxAppDbContext());
AddFefcoBox(fefcoRepository);
Display(fefcoRepository);

static void AddFefcoBox(IRepository<Box> fefcoRepository)
{
    fefcoRepository.Add(new Box { Fefco = 201, Length = 80, Width = 70, Height = 40, Flute = "E", Grammage = 360 });
    fefcoRepository.Add(new Box { Fefco = 201, Length = 90, Width = 60, Height = 80, Flute = "B", Grammage = 420 });
    fefcoRepository.Add(new Box { Fefco = 201, Length = 205, Width = 115, Height = 60, Flute = "EB", Grammage = 650 });
    fefcoRepository.Save();
}

static void Display(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
        Console.WriteLine(item);
}