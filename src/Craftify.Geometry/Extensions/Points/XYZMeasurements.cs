using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZMeasurements
{
    public static double MeasureDistanceAlongVector(
        this XYZ firstPoint, XYZ secondPoint, XYZ vector)
    {
        return Math.Abs(
            firstPoint.ToVector(secondPoint).DotProduct(vector));
    }
    
    public static double MeasureSignedDistance(this XYZ firstPoint, XYZ secondPoint, XYZ vector)
    {
        return firstPoint.ToVector(secondPoint).DotProduct(vector);
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
    public static (XYZ RotationAxis, double Angle) CalculateAlignmentResultTo(this XYZ vectorToAlign, XYZ targetVector)
    {
        var rotationAxis = targetVector.CrossProduct(vectorToAlign);
        rotationAxis = rotationAxis.IsZeroLength() ? XYZ.BasisZ : rotationAxis;
        var angle = targetVector.AngleTo(vectorToAlign);
        return (rotationAxis, angle);
    }
}