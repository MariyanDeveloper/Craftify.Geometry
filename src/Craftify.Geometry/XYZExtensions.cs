using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Shared;

namespace Craftify.Geometry;

public static class XYZExtensions
{
    public static Curve AsCurve(
        this XYZ vector, XYZ? origin = null, double? length = null)
    {
        origin ??= XYZ.Zero;
        length ??= vector.GetLength();
        return Line.CreateBound(
            origin,
            origin.MoveAlongVector(vector, length.GetValueOrDefault()));
    }
    public static XYZ MoveAlongVector(
        this XYZ pointToMove, XYZ vector) => pointToMove.Add(vector);
    public static XYZ MoveAlongVector(
        this XYZ pointToMove, XYZ vector, double distance) => pointToMove.Add(vector.Normalize() * distance);


    public static void VisualizeIn(
        this XYZ point, Document document)
    {
        document.CreateDirectShape(Point.Create(point));
    }
        
    public static VectorRelation GetRelationTo(
        this XYZ fromVector, XYZ toVector)
    {
        if (fromVector.DotProduct(toVector).IsAlmostEqualTo(1))
        {
            return VectorRelation.Equal;
        }
        if (fromVector.DotProduct(toVector).IsAlmostEqualTo(-1))
        {
            return VectorRelation.Reversed;
        }
        if (fromVector.DotProduct(toVector).IsAlmostEqualTo(0))
        {
            return VectorRelation.Perpendicular;
        }
        return VectorRelation.Undefined;
    }
            
    public static XYZ ToVector(
        this XYZ firstPoint, XYZ secondPoint)
    {
        return (secondPoint - firstPoint);
    }
    
    public static XYZ ToNormalizedVector(
        this XYZ firstPoint, XYZ secondPoint)
    {
        return (secondPoint - firstPoint).Normalize();
    }
    
    public static double ToDistanceAlongVector(
        this XYZ firstPoint, XYZ secondPoint, XYZ vector)
    {
        return Math.Abs(
            firstPoint.ToVector(secondPoint).DotProduct(vector));
    }
    
    public static double GetSignedDistance(
        this XYZ firstPoint, XYZ secondPoint, XYZ vector)
    {
        return firstPoint.ToVector(secondPoint).DotProduct(vector);
    }
    
    public static XYZ ProjectOntoPlane(
        this XYZ pointToProject, Plane plane)
    {
        var distance = plane.Origin.GetSignedDistance(
            pointToProject, plane.Normal);
        var projectedPoint = pointToProject - distance * plane.Normal;
        return projectedPoint;
    }
    public static bool LiesOnCurve(
        this XYZ point, Curve curve)
    {
        return curve.Distance(point).IsAlmostEqualTo(0);
    }
    public static AlignmentResult GetAlignmentResultTo(this XYZ vectorToAlign, XYZ targetVector)
    {
        var rotationAxis = targetVector.CrossProduct(vectorToAlign);
        rotationAxis = rotationAxis.IsZeroLength() ? XYZ.BasisZ : rotationAxis;
        var angle = targetVector.AngleTo(vectorToAlign);
        return new AlignmentResult(rotationAxis, angle);
    }
    public static XYZ GetMinByCoordinates(
        this IList<XYZ> points)
    {
        var minPoint = new XYZ(
            points.Min(x => x.X),
            points.Min(x => x.Y),
            points.Min(x => x.Z));
        return minPoint;
    }

    public static XYZ GetMaxByCoordinates(
        this IList<XYZ> points)
    {
        var minPoint = new XYZ(
            points.Max(x => x.X),
            points.Max(x => x.Y),
            points.Max(x => x.Z));
        return minPoint;
    }
}