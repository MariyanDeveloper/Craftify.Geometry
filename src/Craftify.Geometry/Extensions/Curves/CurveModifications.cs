using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions.Curves.Constants;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveModifications
{
    public static Curve ProjectOntoPlaneByCenter(this Curve curve, Plane plane)
    {
        var distance = curve.GetCenter().MeasureSignedDistance(
            plane.Origin,
            plane.Normal);
        var vectorToMoveBy = plane.Normal.Multiply(distance);
        return curve.CreateTransformed(Transform.CreateTranslation(vectorToMoveBy));
    }
    
    public static Curve Extend(
        this Curve test,
        double value,
        Extension extension = Extension.Both)
    {
        var clonedCurve = test.Clone();
        var (startParameter, endParameter) = clonedCurve.CalculateBounds(value, extension);
        clonedCurve.MakeBound(startParameter, endParameter);
        return clonedCurve;
    }

    private static (double StartParameter, double EndParameter) CalculateBounds(
        this Curve curve,
        double value,
        Extension extension)
    {
        var startParameter = curve.GetEndParameter(CurveParameterIndexes.Start);
        var endParameter = curve.GetEndParameter(CurveParameterIndexes.End);
        
        if (extension == Extension.Both)
        {
            return (startParameter - value, endParameter + value);
        }

        if (extension == Extension.Start)
        {
            return (startParameter - value, endParameter);
        }
        
        return (startParameter, endParameter + value);

    }
}