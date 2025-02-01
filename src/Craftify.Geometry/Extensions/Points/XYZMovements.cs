using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Common.ComparisonCalculations;
using Craftify.Geometry.Extensions.Curves;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZMovements
{
    public static (XYZ Left, XYZ Right) SelectFurthermostPointsAlongVector(
        this IEnumerable<XYZ> points,
        XYZ vectorToMeasureAlong,
        Func<XYZ, XYZ, XYZ, double>? measureDistanceFunc = default)
    {
        measureDistanceFunc ??= (fromPoint, toPoint, vector) => fromPoint.MeasureDistanceAlongVector(toPoint, vector);
        return points
            .SelectFurthermostPair(
                (fromPoint, toPoint) => measureDistanceFunc(fromPoint, toPoint, vectorToMeasureAlong));
    }
    
    public static (XYZ Left, XYZ Right) SelectFurthermostPoints(
        this IEnumerable<XYZ> points,
        Func<XYZ, XYZ, double>? measureDistanceFunc = default)
    {
        measureDistanceFunc ??= (fromPoint, toPoint) => fromPoint.DistanceTo(toPoint);
        return points
            .SelectFurthermostPair(
                measureDistanceFunc);
    }
    
    public static XYZ MoveAlongVector(
        this XYZ pointToMove, XYZ vector) => pointToMove.Add(vector);
    public static XYZ MoveAlongVector(
        this XYZ pointToMove, XYZ vector, double distance) => pointToMove.Add(vector.Normalize() * distance);
    
    public static XYZ ProjectOntoPlane(
        this XYZ pointToProject, Plane plane)
    {
        var distance = plane.Origin.MeasureSignedDistance(
            pointToProject, plane.Normal);
        var projectedPoint = pointToProject - distance * plane.Normal;
        return projectedPoint;
    }
}