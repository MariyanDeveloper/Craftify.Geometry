using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Curves.Constants;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveOperationExtensions
{
    
    public static XYZ ToNormalizedVector(
        this Curve curve)
    {
        return curve.ToVector().Normalize();
    }
    public static XYZ ToVector(this Curve curve) =>
        curve.GetEndPoint(CurveParameterIndexes.End) - curve.GetEndPoint(CurveParameterIndexes.Start);

    public static IEnumerable<XYZ> SelectVertices(this Curve curve) => curve.Tessellate();

    public static XYZ GetCenter(this Curve curve) => curve.Evaluate(CurveParameterIndexes.Center, true);
    public static XYZ GetStartPoint(this Curve curve) => curve.GetEndPoint(CurveParameterIndexes.Start);
    public static XYZ GetEndPoint(this Curve curve) => curve.GetEndPoint(CurveParameterIndexes.End);
}