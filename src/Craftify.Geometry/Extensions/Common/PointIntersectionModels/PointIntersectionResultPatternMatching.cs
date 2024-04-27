using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Common.Intersections;

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