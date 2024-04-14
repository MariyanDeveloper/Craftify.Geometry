using Autodesk.Revit.DB;
using System;
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

    public static Transform ToTransformAsYFacing(this XYZ vector)
    {
        if (vector is null) throw new ArgumentNullException(nameof(vector));
        var defaultVector = XYZ.BasisZ;
        if (defaultVector.IsAlmostEqualTo(vector))
        {
            return TransformReturns.CreateYAsZ();
        }
        var yAxis = vector.Normalize();
        var zAxis = defaultVector.CrossProduct(yAxis).Normalize();
        var xAxis = yAxis.CrossProduct(zAxis).Normalize();
        var transform = Transform.Identity;
        transform.BasisX = xAxis;
        transform.BasisY = yAxis;
        transform.BasisZ = zAxis;
        return transform;
    }
}