using System.Collections.Generic;

namespace Craftify.Geometry.EnumerableExtensions;

public static class List
{
    public static IEnumerable<T> Of<T>(params T[] items) => new List<T>(items);
}