using BoxApp.Data.Entities;

namespace BoxApp.Components.DataProviders.Extensions;

public static class BoxesHelper
{
    public static IEnumerable<Box> ByLength(this IEnumerable<Box> query, int length)
    {
        return query.Where(box => box.Length == length);
    }
}
