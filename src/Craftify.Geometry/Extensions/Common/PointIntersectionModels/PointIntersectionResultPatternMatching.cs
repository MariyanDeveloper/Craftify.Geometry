using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.EnumerableExtensions;

namespace Craftify.Geometry.Extensions.Common.Intersections;

public static class PointIntersectionResultMapExtensions
{
    public static IEnumerable<XYZ> ToPoints(this PointIntersectionResult pointIntersectionResult) =>
        pointIntersectionResult.Match(
            noIntersection: Enumerable.Empty<XYZ>,
            exactIntersection: point => point.AsMaterializedEnumerable(),
            multiIntersection: points => points);

    public static PointIntersectionResult ToPointIntersectionResult(this IEnumerable<XYZ> points) =>
        points.Match(
            emptyCase: PointIntersectionResults.CreateNoIntersection,
            singleCase: PointIntersectionResults.CreateExactIntersection,
            multipleCase: PointIntersectionResults.CreateMultiIntersection);
}

public static class PointIntersectionResultPatternMatching
{
    public static T Match<T>(
        this PointIntersectionResult result,
        Func<T> noIntersection,
        Func<XYZ, T> exactIntersection,
        Func<IReadOnlyCollection<XYZ>, T> multiIntersection)
    {
        return result switch
        {
            ExactIntersectionResult exactIntersectionResult => exactIntersection(exactIntersectionResult.IntersectionPoint),
            MultiPointIntersectionResult multiIntersectionResult => multiIntersection(multiIntersectionResult.IntersectionPoints),
            NoIntersectionResult noIntersectionResult => noIntersection(),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    }
    
    public static T Match<T>(
        this PointIntersectionResult result,
        Func<NoIntersectionResult, T> noIntersection,
        Func<ExactIntersectionResult, T> exactIntersection,
        Func<MultiPointIntersectionResult, T> multiIntersection)
    {
        return result switch
        {
            ExactIntersectionResult exactIntersectionResult => exactIntersection(exactIntersectionResult),
            MultiPointIntersectionResult multiIntersectionResult => multiIntersection(multiIntersectionResult),
            NoIntersectionResult noIntersectionResult => noIntersection(noIntersectionResult),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    }
}