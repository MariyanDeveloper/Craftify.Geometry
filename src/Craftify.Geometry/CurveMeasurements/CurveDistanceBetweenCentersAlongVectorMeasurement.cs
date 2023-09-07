using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Curves;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.CurveMeasurements;

public class CurveDistanceBetweenCentersAlongVectorMeasurement : ICurveDistanceAlongVectorMeasurement
{
    public double Measure(Curve fromCurve, Curve toCurve, XYZ vectorToMeasureBy)
    {
        return fromCurve.GetDistanceBetweenCentersAlongVectorTo(toCurve, vectorToMeasureBy);
    }
}