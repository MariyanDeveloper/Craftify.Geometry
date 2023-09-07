using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.CurveMeasurements;
using Craftify.Geometry.Interfaces;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveAnalysisExtensions
{
    public static bool LiesInPlane(this Curve curve, Plane plane)
    {
        var startPoint = curve.GetStartPoint();
        var endPoint = curve.GetEndPoint();
        plane.Project(startPoint, out _, out var startPointDistance);
        plane.Project(endPoint, out _, out var endPointDistance);
        return startPointDistance.IsAlmostEqualTo(0) &&
               endPointDistance.IsAlmostEqualTo(0);
    }
    
    public static bool IsVertical(this Curve curve)
    {
        var vector = curve.GetNormalizedVector();
        return vector.IsAlmostEqualTo(XYZ.BasisZ) || vector.IsAlmostEqualTo(-XYZ.BasisZ);
    }
    public static double GetDistanceBetweenCentersAlongVectorTo(this Curve fromCurve, Curve toCurve, XYZ vectorToMeasureBy)
    {
        var fromCenter = fromCurve.GetCenter();
        var toCenter = toCurve.GetCenter();
        return fromCenter.MeasureDistanceAlongVector(toCenter, vectorToMeasureBy);
    }
    public static double GetDistanceBetweenCentersTo(this Curve fromCurve, Curve toCurve)
    {
        var fromCenter = fromCurve.GetCenter();
        var toCenter = toCurve.GetCenter();
        return fromCenter.DistanceTo(toCenter);
    }
    
    public static bool IsCollinearTo(this Line fromLine, Line toLine)
    {
        var fromVector = fromLine.Direction;
        var toVector = toLine.Origin - fromLine.Origin;
        return fromVector.IsParallelTo(toLine.Direction) && fromVector.IsParallelTo(toVector);
    }
    
    public static bool IsParallelTo(this XYZ fromVector, XYZ toVector)
    {
        return fromVector.CrossProduct(toVector).IsZeroLength();
    }
    
    public static Curve FindClosest(
        this Curve curve,
        IEnumerable<Curve> toCurves,
        ICurveDistanceMeasurement? curveDistanceMeasurement = null)
    {
        curveDistanceMeasurement ??= CurveDistanceMeasurement.Default;
        var minDistance = double.MaxValue; 
        Curve? closestCurve = null;

        foreach (var toCurve in toCurves)
        {
            var currentDistance = curveDistanceMeasurement.Measure(curve, toCurve);

            if (currentDistance < minDistance is false)
            {
                continue;
            }
            minDistance = currentDistance;
            closestCurve = toCurve;
        }

        return closestCurve;
    }
    public static FurthestCurvesResult FindFurthestApart(
        this List<Curve> curves,
        XYZ vectorToMeasureBy,
        ICurveDistanceAlongVectorMeasurement? measurement = null)
    {
        measurement ??= CurveDistanceAlongVectorMeasurement.Default;
        var maxDistance = double.MinValue;
        Curve? firstCurve = default; 
        Curve? secondCurve = default;

        for (var i = 0; i < curves.Count; i++)
        {
            for (var j = i + 1; j < curves.Count; j++)
            {
                var distance = measurement.Measure(
                    curves[i],
                    curves[j],
                    vectorToMeasureBy);
                if (distance > maxDistance is false)
                {
                    continue;
                }
                maxDistance = distance;
                firstCurve = curves[i];
                secondCurve = curves[j];
            }
        }

        if (firstCurve is null || secondCurve is null)
        {
            throw new NullReferenceException($"Curves cannot be null");
        }

        return new FurthestCurvesResult(firstCurve, secondCurve);
    }
    
    public static Line CreateMergedWithCollinear(this Line fromLine, Line toLine)
    {
        if (fromLine.IsCollinearTo(toLine) is false)
        {
            throw new ApplicationException($"Lines should be collinear");
        }
        var allPoints = new List<XYZ>();
        allPoints.AddRange(fromLine.Tessellate());
        allPoints.AddRange(toLine.Tessellate());
        var furthermostResult = allPoints.GetFurthestPoints();
        return Line.CreateBound(furthermostResult.Left, furthermostResult.Right);
    }
    public static bool IntersectsWith(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve.Intersect(toCurve) == SetComparisonResult.Overlap;
    }
    public static bool CompletelyInside(this Curve fromCurve, Curve toCurve)
    {
        var vertices = fromCurve.GetVertices();
        return vertices.All(v => v.LiesOnCurve(toCurve));
    }
    
    public static List<XYZ> GetIntersectedPointsIfHas(this Curve fromCurve, IEnumerable<Curve> curves)
    {
        return curves
            .Where(c => c.IntersectsWith(fromCurve))
            .Select(c => c.GetIntersectedPoint(fromCurve))
            .ToList();
    }
    public static bool TryGetIntersectedPoint(
        this Curve fromCurve, Curve toCurve, out XYZ intersectedPoint)
    {
        intersectedPoint = default;
        var intersectionResult = fromCurve.Intersect(toCurve, out var resultArray);
        if (intersectionResult != SetComparisonResult.Overlap)
        {
            return false;
        }
        intersectedPoint = resultArray.OfType<IntersectionResult>().Select(x => x.XYZPoint).First();
        return true;

    }
    public static XYZ GetIntersectedPoint(
        this Curve fromCurve, Curve toCurve)
    {
        if (fromCurve.TryGetIntersectedPoint(toCurve, out var resultPoint) is false)
        {
            throw new ArgumentNullException("Curves don't intersect");
        }
        return resultPoint;
    }

    public static bool IsContinuousWith(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve
            .GetEndPoint()
            .IsAlmostEqualTo(
                toCurve.GetStartPoint());
    }
    public static bool ShareSameEndPoints(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve
            .GetEndPoint()
            .IsAlmostEqualTo(
                toCurve.GetEndPoint());
    }
}