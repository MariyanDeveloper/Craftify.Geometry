using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Common.PointIntersectionModels;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveIntersections
{
    public static PointIntersectionResult FindIntersectionWithCurves(
        this Curve fromCurve, IEnumerable<Curve> toCurves)
    {
        return toCurves.FindIntersections(
            fromCurve,
            (givenToCurve, givenFromCurve) => givenToCurve.FindIntersectionWithCurve(givenFromCurve));
    }
    
    public static PointIntersectionResult FindIntersectionWithCurves(
        this IEnumerable<Curve> fromCurves, Curve toCurve)
    {
        return toCurve.FindIntersectionWithCurves(fromCurves);
    }
    
    public static PointIntersectionResult FindIntersectionWithCurve(
        this Curve fromCurve, Curve toCurve)
    {
        return fromCurve
            .GetIntersectionResult(toCurve)
            .MapToPointIntersectionResult();
    }
    
    public static (SetComparisonResult ComparisonResult, IntersectionResultArray IntersectionResultArray) GetIntersectionResult(
        this Curve fromCurve, Curve toCurve)
    {
        var result = fromCurve.Intersect(toCurve, out var resultArray);
        return (result, resultArray);
    }

}