using Autodesk.Revit.DB;

namespace Craftify.Geometry.Interfaces;

public interface ICurveDistanceAlongVectorMeasurement
{
    double Measure(Curve fromCurve, Curve toCurve, XYZ vectorToMeasureBy);
}