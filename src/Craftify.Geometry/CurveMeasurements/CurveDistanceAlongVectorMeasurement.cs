using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.CurveMeasurements;

public static class CurveDistanceAlongVectorMeasurement
{
    public static ICurveDistanceAlongVectorMeasurement Default =>
        new CurveDistanceBetweenCentersAlongVectorMeasurement();
}