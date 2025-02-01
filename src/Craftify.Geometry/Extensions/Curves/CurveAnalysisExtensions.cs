using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Common.ComparisonCalculations;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveAnalysisExtensions
{
    public static T SelectClosestTo<T>(
        this T curve,
        IEnumerable<T> curves, 
        Func<T, T, double>? measureDistanceFunc = default)
        where T : Curve
    {
        measureDistanceFunc ??= (curve1, curve2) => curve1.MeasureDistanceBetweenCentersTo(curve2);
        return curve.SelectClosestElement(
            elements: curves,
            measureDistanceFunc: measureDistanceFunc);
    }
    
    public static (T Left, T Right) SelectClosestAlongVector<T>(
        this IEnumerable<T> curves,
        XYZ vector,
        Func<T, T, XYZ, double>? measureDistanceFunc = default)
        where T : Curve
    {
        measureDistanceFunc ??= (from, to, alongVector) => from.MeasureDistanceBetweenCentersAlongVectorTo(to, alongVector);
        return curves
            .SelectClosestPairAlongVector(
                vectorToMeasureBy: vector,
                vectorMeasurementFunc: measureDistanceFunc);
    }

    public static (T Left, T Right) SelectFurthermostAlongVector<T>(
        this IEnumerable<T> curves,
        XYZ vector,
        Func<T, T, XYZ, double>? measureDistanceFunc = default)
        where T : Curve
    {
        measureDistanceFunc ??= (from, to, alongVector) => from.MeasureDistanceBetweenCentersAlongVectorTo(to, alongVector);
        return curves
            .SelectFurthermostPairAlongVector(
                vectorToMeasureBy: vector,
                vectorMeasurementFunc: measureDistanceFunc);
    }
    
    public static Line MergeWithCollinear(this Line fromLine, Line toLine)
    {
        if (fromLine.IsCollinearTo(toLine) is false)
        {
            throw new InvalidOperationException($"Lines are not collinear");
        }
        var (left, right) = fromLine.Tessellate()
            .Concat(toLine.Tessellate())
            .SelectFurthermostPair(
                measureDistanceFunc: (fromPoint, toPoint) => fromPoint.DistanceTo(toPoint));
        return Line.CreateBound(
            left, right);
    }

}