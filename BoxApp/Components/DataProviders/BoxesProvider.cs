using BoxApp.Components.DataProviders.Extensions;
using BoxApp.Data.Entities;
using BoxApp.Data.Repositories;

namespace BoxApp.Components.DataProviders;

internal class BoxesProvider(IRepository<Box> boxesRepository) : IBoxesProvider
{
    private readonly IRepository<Box> _boxesRepository = boxesRepository;

    // SELECT
    public int GetMaxLengthOfAllBoxes()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Select(box => box.Length).Max();
    }

    public int GetMinHeightOfAllBoxes()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Select(box => box.Height).Min();
    }

    public List<Box> GetSpecificColumns()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Select(box => new Box
        {
            Id = box.Id,
            Length = box.Length,
            Width = box.Width,
            Height = box.Height,
        }).ToList();
    }

    public List<int> GetUniqueBoxGrammage()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Select(x => x.Grammage).Distinct().ToList();
    }

    // ORDER BY

    public List<Box> OrderByLength()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.OrderBy(box => box.Length).ToList();
    }

    public List<Box> OrderByWidth()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.OrderBy(box => box.Width).ToList();
    }

    public List<Box> OrderByHeight()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.OrderBy(box => box.Height).ToList();
    }

    public List<Box> OrderByLengthDescending()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.OrderByDescending(box => box.Length).ToList();
    }

    public List<Box> OrderByWidthAndHeight()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes
            .OrderBy(box => box.Width)
            .ThenBy(box => box.Height)
            .ToList();
    }

    public List<Box> OrderByWidthAndLengthDescending()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes
            .OrderBy(box => box.Width)
            .ThenByDescending(box => box.Length)
            .ToList();
    }

    // WHERE
    public List<Box> FilterBoxes(int minLength)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Where(x => x.Length > minLength).ToList();
    }

    public List<Box> GetAllBoxesWithSpecificHeight(int Height)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Where(box => box.Height == Height).ToList();
    }

    public List<Box> GetAllBoxesWithWidthMaxAndLengthMin(int Width, int Length)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Where(box => box.Width < Width && box.Length > Length).ToList();
    }

    public List<Box> WhereLengthIs(int length)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.ByLength(length).ToList();
    }

    // FIRST, LAST, SINGLE
    public Box FirstByLength(int length)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.First(box => box.Length == length);
    }

    public Box? FirstOrDefaultByLength(int length)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.FirstOrDefault(box => box.Length == length);
    }

    public Box FirstOrDefaultByLengthWithDefault(int length)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.FirstOrDefault(box => box.Length == length, new Box { Id = -1, Length = 300 });
    }

    public Box LastByWidth(int width)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Last(box => box.Width == width);
    }

    public Box SingleById(int id)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Single(box => box.Id == id);
    }

    public Box? SingleOrDefaultById(int id)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.SingleOrDefault(box => box.Id == id);
    }

    // TAKE
    public List<Box> TakeBoxes(int howMany)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Take(howMany).ToList();
    }

    public List<Box> TakeBoxes(Range range)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Take(range).ToList();
    }

    public List<Box> TakeBoxesWhileLengthIsMoreThan(int length)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.TakeWhile(box => box.Length < length).ToList();
    }

    // SKIP
    public List<Box> SkipBoxes(int howMany)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Skip(howMany).ToList();
    }

    public List<Box> SkipBoxesWhileHeightIsLessOrEqualThan(int height)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.SkipWhile(box => box.Height <= height).ToList();
    }

    // DISTINCT
    public List<string?> DistinctAllFlutes()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes
            .Select(box => box.Flute)
            .Distinct()
            .ToList();
    }

    public List<Box> DistinctByGrammage()
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.DistinctBy(box => box.Grammage).ToList();
    }

    // CHUNK
    public List<Box[]> ChunkBoxes(int size)
    {
        var boxes = _boxesRepository.GetAll();
        return boxes.Chunk(size).ToList();
    }
}
