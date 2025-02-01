using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Common.PointIntersectionModels;

public static class IntersectionExtensions
{
    public static PointIntersectionResult MapToPointIntersectionResult(
        this (SetComparisonResult ComparisonResult, IntersectionResultArray IntersectionResultArray) intersectionResult)
    {
        var (comparisonResult, intersectionResultArray) = intersectionResult;
        if (comparisonResult != SetComparisonResult.Overlap)
        {
            return PointIntersectionResults.CreateNoIntersection();
        }
        return intersectionResultArray
            .SelectIntersectionPoints()
            .Match(
                emptyCase: PointIntersectionResults.CreateNoIntersection,
                singleCase: PointIntersectionResults.CreateExactIntersection,
                multipleCase: PointIntersectionResults.CreateMultiIntersection);
    }

    public static PointIntersectionResult FindIntersections<TFrom, TTo>(
        this IEnumerable<TFrom> fromElements,
        TTo toElement,
        Func<TFrom, TTo, PointIntersectionResult> pointIntersectionResultProvider)
    {
        return fromElements
            .SelectMany(x => pointIntersectionResultProvider(x, toElement)
                .ToPoints())
            .ToPointIntersectionResult();
    }
}