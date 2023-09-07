using Autodesk.Revit.DB;

namespace Craftify.Geometry.Interfaces;

public interface ICurveDistanceMeasurement
{
    double Measure(Curve fromCurve, Curve toCurve);
}