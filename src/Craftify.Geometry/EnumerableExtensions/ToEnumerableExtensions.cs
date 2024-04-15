using System;
using System.Collections.Generic;
using System.Linq;
using static Craftify.Geometry.EnumerableExtensions.EnumerableReturns;
namespace Craftify.Geometry.EnumerableExtensions;

public static class ToEnumerableExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this T value)
    {
        yield return value;
    }
    public static IEnumerable<T> AsMaterializedEnumerable<T>(this T value) => List(value);
}

public static class EnumerablePatternMatching
{
    public static TResult Match<T, TResult>(
        this IEnumerable<T> list,
        Func<TResult> emptyCase,
        Func<T, TResult> singleCase,
        Func<IReadOnlyCollection<T>, TResult> multipleCase)
    {
        var materializedList = list.ToArray();

        if (!materializedList.Any())
        {
            return emptyCase();
        }
        if (materializedList.Length == 1)
        {
            return singleCase(materializedList[0]);
        }

        return multipleCase(materializedList);
    }
}