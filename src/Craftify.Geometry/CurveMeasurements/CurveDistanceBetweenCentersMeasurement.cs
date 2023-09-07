using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Curves;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.CurveMeasurements;

public class CurveDistanceBetweenCentersMeasurement : ICurveDistanceMeasurement
{
    public double Measure(Curve fromCurve, Curve toCurve)
    {
        return fromCurve.GetDistanceBetweenCentersTo(toCurve);
    }
}