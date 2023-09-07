using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;

namespace Craftify.Geometry.Extensions;

public static class FaceExtensions
{
    public static void VisualizeIn(this Face face, Document document)
    {
        if (face is null) throw new ArgumentNullException(nameof(face));
        if (document is null) throw new ArgumentNullException(nameof(document));
        face
            .GetEdgesAsCurveLoops()
            .SelectMany(x => x)
            .VisualizeIn(document);
    }
    public static void VisualizeIn(this IEnumerable<Face> faces, Document document)
    {
        if (faces is null) throw new ArgumentNullException(nameof(faces));
        if (document is null) throw new ArgumentNullException(nameof(document));
        foreach (var face in faces)
        {
            face.VisualizeIn(document);
        }
    }
    
    public static bool DoesIntersectWithCurve(
        this Face face, Curve curve)
    {
        var comparisonResult = face.Intersect(curve, out _);
        return comparisonResult == SetComparisonResult.Overlap;
    }
    public static IntersectedPoints GetIntersectedPointsWithCurve(
        this IEnumerable<Face> faces, Curve curve)
    {
        var intersectedPoints = faces
            .Select(x => (
                DoesIntersect: x.TryGetIntersectedPointsWithCurve(curve, out var result),
                IntersectedPoints: result))
            .Where(x => x.DoesIntersect)
            .SelectMany(x => x.IntersectedPoints)
            .ToIntersectedPoints();
        return intersectedPoints;
    }
    
    public static bool TryGetIntersectedPointsWithCurve(
        this Face face, Curve curve, out IntersectedPoints result)
    {
        result = new IntersectedPoints();
        var comparisonResult = face.Intersect(curve, out var resultArray);
        if (comparisonResult != SetComparisonResult.Overlap)
        {
            return false;
        }
        result = resultArray.OfType<IntersectionResult>()
            .Select(x => x.XYZPoint).ToIntersectedPoints();
        return true;
    }
    
    public static UV GetCenterNormalUV(
        this Face face)
    {
        var uvBounds = Enumerable.Range(0, 2)
            .Select(i => face.GetBoundingBox().get_Bounds(i))
            .ToArray();
        var normalUV = new UV(
            uvBounds.Sum(uv => uv.U) / 2,
            uvBounds.Sum(uv => uv.V) / 2
        );
        return normalUV;
    }
    
    public static XYZ GetCenterNormal(
        this Face face)
    {
        return face.ComputeNormal(face.GetCenterNormalUV());
    }
    
    public static XYZ GetCenterPoint(
        this Face face)
    {
        return face.Evaluate(face.GetCenterNormalUV());
    }
    
    public static Curve GetCenterNormalAsCurve(
        this Face face)
    {
        var centerPoint = face.GetCenterPoint();
        return Line.CreateBound(centerPoint, centerPoint.MoveAlongVector(face.GetCenterNormal()));
    }

    public static Plane GetPlaneAtCenter(this Face face)
    {
        return Plane.CreateByNormalAndOrigin(face.GetCenterNormal(), face.GetCenterPoint());
    }
}