using Autodesk.Revit.DB;
using System;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZTransformAlignments
{
    public static Transform AlignToTransformAsYFacing(this XYZ vector)
    {
        if (vector is null) throw new ArgumentNullException(nameof(vector));
        var defaultVector = XYZ.BasisZ;
        if (defaultVector.IsAlmostEqualTo(vector))
        {
            return Transforms.Transforms.CreateYAsZ();
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
    
    public static Transform AlignToTransformAsXFacing(this XYZ vector)
    {
        if (vector is null) throw new ArgumentNullException(nameof(vector));
        var vectorUp = XYZ.BasisZ;
        if (vectorUp.IsAlmostEqualTo(vector))
        {
            return Transforms.Transforms.CreateXAsZ();
        }
        var xAxis = vector.Normalize();
        var yAxis = vectorUp.CrossProduct(xAxis).Normalize();
        var zAxis = xAxis.CrossProduct(yAxis).Normalize();
        var transform = Transform.Identity;
        transform.BasisX = xAxis;
        transform.BasisY = yAxis;
        transform.BasisZ = zAxis;
        return transform;
    }
    public static Transform AlignToTransformAsZFacing(this XYZ vector)
    {
        var zAxis = vector.Normalize();
        var xAxis = XYZ.BasisX.CrossProduct(zAxis).Normalize();
        var yAxis = zAxis.CrossProduct(xAxis).Normalize();
        var transform = Transform.Identity;
        transform.BasisX = xAxis;
        transform.BasisY = yAxis;
        transform.BasisZ = zAxis;
        return transform;
    }
}