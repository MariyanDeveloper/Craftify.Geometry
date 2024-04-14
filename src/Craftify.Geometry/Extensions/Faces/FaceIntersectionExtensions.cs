using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Common.Intersections;

namespace Craftify.Geometry.Extensions.Faces;

public static class FaceIntersectionExtensions
{
    public static bool DoesIntersectWithCurve(
        this Face face, Curve curve)
    {
        var comparisonResult = face.Intersect(curve, out _);
        return comparisonResult == SetComparisonResult.Overlap;
    }
    
    public static PointIntersectionResult FindIntersectionWithCurve(
        this IEnumerable<Face> faces,
        Curve curve)
    {
        return faces.FindIntersections(
            curve,
            (givenFace, givenCurve) => givenFace.FindIntersectionWithCurve(givenCurve)
        );
    }
    
    public static PointIntersectionResult FindIntersectionWithFaces(
        this Curve curve,
        IEnumerable<Face> faces)
    {
        return faces.FindIntersectionWithCurve(curve);
    }

    public static (SetComparisonResult ComparisonResult, IntersectionResultArray IntersectionResultArray) GetIntersectionResult(
            this Face face, Curve curve)
    {
        var result = face.Intersect(curve, out var resultArray);
        return (result, resultArray);
    }
    

    public static PointIntersectionResult FindIntersectionWithCurve(
        this Face face, Curve curve)
    {
        return face
            .GetIntersectionResult(curve)
            .MapToPointIntersectionResult();
    }
}