using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveMeasurementExtensions
{
    public static double MeasureDistanceBetweenCentersAlongVectorTo(this Curve fromCurve, Curve toCurve, XYZ vectorToMeasureBy)
    {
        var fromCenter = fromCurve.GetCenter();
        var toCenter = toCurve.GetCenter();
        return fromCenter.MeasureDistanceAlongVector(toCenter, vectorToMeasureBy);
    }
    public static double MeasureDistanceBetweenCentersTo(this Curve fromCurve, Curve toCurve)
    {
        var fromCenter = fromCurve.GetCenter();
        var toCenter = toCurve.GetCenter();
        return fromCenter.DistanceTo(toCenter);
    }
}