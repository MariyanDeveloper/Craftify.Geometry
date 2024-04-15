using System.Collections.Generic;

namespace Craftify.Geometry.EnumerableExtensions;

public static class EnumerableReturns
{
    public static IEnumerable<T> List<T>(params T[] items) => new List<T>(items);
}