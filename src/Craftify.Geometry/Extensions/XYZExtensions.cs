using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Interfaces;
using Craftify.Geometry.Models;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions;

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

    public static void VisualizeIn(this IEnumerable<XYZ> points, Document document)
    {
        document.CreateDirectShape(points.Select(Point.Create));
    }
        
    public static VectorRelation CalculateRelationTo(
        this XYZ fromVector, XYZ toVector)
    {
        var signOfVectorEquality = 1;
        var signOfVectorReversion = -1;
        var signOfVectorPerpendicularity = 0;
        
        if (fromVector.DotProduct(toVector).IsAlmostEqualTo(signOfVectorEquality))
        {
            return VectorRelation.Equal;
        }
        if (fromVector.DotProduct(toVector).IsAlmostEqualTo(signOfVectorReversion))
        {
            return VectorRelation.Reversed;
        }
        if (fromVector.DotProduct(toVector).IsAlmostEqualTo(signOfVectorPerpendicularity))
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
    
    public static XYZ GetNormalizedVectorTo(
        this XYZ firstPoint, XYZ secondPoint)
    {
        return (secondPoint - firstPoint).Normalize();
    }
    
    public static double MeasureDistanceAlongVector(
        this XYZ firstPoint, XYZ secondPoint, XYZ vector)
    {
        return Math.Abs(
            firstPoint.ToVector(secondPoint).DotProduct(vector));
    }
    
    public static double MeasureSignedDistance(
        this XYZ firstPoint, XYZ secondPoint, XYZ vector)
    {
        return firstPoint.ToVector(secondPoint).DotProduct(vector);
    }
    
    public static XYZ ProjectOntoPlane(
        this XYZ pointToProject, Plane plane)
    {
        var distance = plane.Origin.MeasureSignedDistance(
            pointToProject, plane.Normal);
        var projectedPoint = pointToProject - distance * plane.Normal;
        return projectedPoint;
    }
    public static bool LiesOnCurve(
        this XYZ point, Curve curve)
    {
        return curve.Distance(point).IsAlmostEqualTo(0);
    }
    public static AlignmentResult CalculateAlignmentResultTo(this XYZ vectorToAlign, XYZ targetVector)
    {
        var rotationAxis = targetVector.CrossProduct(vectorToAlign);
        rotationAxis = rotationAxis.IsZeroLength() ? XYZ.BasisZ : rotationAxis;
        var angle = targetVector.AngleTo(vectorToAlign);
        return new AlignmentResult(rotationAxis, angle);
    }
    public static XYZ GetMinByCoordinates(
        this ICollection<XYZ> points)
    {
        var minPoint = new XYZ(
            points.Min(x => x.X),
            points.Min(x => x.Y),
            points.Min(x => x.Z));
        return minPoint;
    }

    public static XYZ GetMaxByCoordinates(
        this ICollection<XYZ> points)
    {
        var minPoint = new XYZ(
            points.Max(x => x.X),
            points.Max(x => x.Y),
            points.Max(x => x.Z));
        return minPoint;
    }
    
    public static Transform AlignToTransform(this XYZ vector, IVectorToTransformAlignment vectorToTransformAlignment)
    {
        if (vector is null) throw new ArgumentNullException(nameof(vector));
        if (vectorToTransformAlignment is null) throw new ArgumentNullException(nameof(vectorToTransformAlignment));
        return vectorToTransformAlignment.Align(vector);
    }

    public static Coordinates GetCoordinates(this XYZ xyz)
    {
        return new Coordinates(Enumerable.Range(0, 3).Select(x => xyz[x]));
    }
    public static Vertices ToVertices(this IEnumerable<XYZ> points) => new(points);
    
    public static FurthermostResult GetFurthestPoints(this List<XYZ> points)
    {
        XYZ point1 = null, point2 = null;
        double maxDistance = 0;

        for (var i = 0; i < points.Count; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                var distance = points[i].DistanceTo(points[j]);
                if (distance > maxDistance is false)
                {
                    continue;
                }
                maxDistance = distance;
                point1 = points[i];
                point2 = points[j];
            }
        }

        return new FurthermostResult(point1, point2);
    }
}
public class FurthermostResult
{
    public XYZ Left { get; }
    public XYZ Right { get; }

    public FurthermostResult(XYZ left, XYZ right)
    {
        Left = left;
        Right = right;
    }
}