using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.CurveMeasurements;

public static class CurveDistanceMeasurement
{
    public static ICurveDistanceMeasurement Default => new CurveDistanceBetweenCentersMeasurement();
}