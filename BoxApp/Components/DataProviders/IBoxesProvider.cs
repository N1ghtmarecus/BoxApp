using BoxApp.Data.Entities;

namespace BoxApp.Components.DataProviders;

public interface IBoxesProvider
{
    // SELECT
    int GetMaxLengthOfAllBoxes();

    int GetMinHeightOfAllBoxes();

    List<Box> GetSpecificColumns();

    List<int> GetUniqueBoxGrammage();

    // ORDER BY
    List<Box> OrderByLength();

    List<Box> OrderByWidth();

    List<Box> OrderByHeight();

    List<Box> OrderByLengthDescending();

    List<Box> OrderByWidthAndHeight();

    List<Box> OrderByWidthAndLengthDescending();

    // WHERE

    List<Box> FilterBoxes(int minLength);

    List<Box> GetAllBoxesWithSpecificHeight(int Height);

    List<Box> GetAllBoxesWithWidthMaxAndLengthMin(int Width, int Length);

    List<Box> WhereLengthIs(int length);

    // FIRST, LAST, SINGLE

    Box FirstByLength(int length);

    Box? FirstOrDefaultByLength(int length);

    Box FirstOrDefaultByLengthWithDefault(int length);

    Box LastByWidth(int width);

    Box SingleById(int id);

    Box? SingleOrDefaultById(int id);

    // TAKE

    List<Box> TakeBoxes(int howMany);

    List<Box> TakeBoxes(Range range);

    List<Box> TakeBoxesWhileLengthIsMoreThan(int length);

    //SKIP

    List<Box> SkipBoxes(int howMany);

    List<Box> SkipBoxesWhileHeightIsLessOrEqualThan(int height);

    // DISTINCT

    List<string?> DistinctAllFlutes();

    List<Box> DistinctByGrammage();

    // CHUNK

    List<Box[]> ChunkBoxes(int size);

}
