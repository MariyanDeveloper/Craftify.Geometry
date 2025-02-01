using Autodesk.Revit.DB;
using System;
using Craftify.Geometry.Extensions.Transforms;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZConversions
{
    public static Curve AsCurve(
        this XYZ vector, XYZ? origin = null, double? length = null)
    {
        origin ??= XYZ.Zero;
        length ??= vector.GetLength();
        return Line.CreateBound(
            origin,
            origin.MoveAlongVector(vector, length.GetValueOrDefault()));
    }

    public static Point ToPoint(this XYZ xyz) => Point.Create(xyz);
    
}