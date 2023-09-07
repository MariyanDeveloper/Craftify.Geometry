using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions.Curves.Constants;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveOperationExtensions
{
    public static Curve CreateProjectedOntoPlaneByMovingCenter(this Curve curve, Plane plane)
    {
        var distance = curve.GetCenter().MeasureSignedDistance(
            plane.Origin,
            plane.Normal);
        var vectorToMoveBy = plane.Normal.Multiply(distance);
        return curve.CreateTransformed(Transform.CreateTranslation(vectorToMoveBy));
    }
    
    public static void Extend(
        this Curve curve, double value, Extension extension = Extension.Both)
    {
        var startParameter = curve.GetEndParameter(CurveParameterIndexes.Start);
        var endParameter = curve.GetEndParameter(CurveParameterIndexes.End);
        if (extension == Extension.Both)
        {
            curve.MakeBound(startParameter - value, endParameter + value);
            return;
        }
        if (extension == Extension.Start)
        {
            curve.MakeBound(startParameter - value, endParameter);
            return;
        }
        curve.MakeBound(startParameter, endParameter + value);
    }
    public static Curve ExtendAsCloned(
        this Curve curve, double value, Extension extension = Extension.Both)
    {
        var clonedCurve = curve.Clone();
        clonedCurve.Extend(value, extension);
        return clonedCurve;
    }
    
    public static XYZ GetNormalizedVector(
        this Curve curve)
    {
        return curve.CreateVector().Normalize();
    }
    public static XYZ CreateVector(this Curve curve) =>
        curve.GetEndPoint(CurveParameterIndexes.End) - curve.GetEndPoint(CurveParameterIndexes.Start);

    public static Vertices GetVertices(this Curve curve) => new Vertices(curve.Tessellate());

    public static XYZ GetCenter(this Curve curve) => curve.Evaluate(CurveParameterIndexes.Center, true);
    public static XYZ GetStartPoint(this Curve curve) => curve.GetEndPoint(CurveParameterIndexes.Start);
    public static XYZ GetEndPoint(this Curve curve) => curve.GetEndPoint(CurveParameterIndexes.End);
}