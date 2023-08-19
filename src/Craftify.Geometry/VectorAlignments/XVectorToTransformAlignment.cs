using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.VectorAlignments;

public class XVectorToTransformAlignment : IVectorToTransformAlignment
{
    public Transform Align(XYZ vector)
    {
        if (vector is null) throw new ArgumentNullException(nameof(vector));
        var vectorUp = XYZ.BasisZ;
        if (vectorUp.IsAlmostEqualTo(vector))
        {
            return new TransformBuilder().BuildXAsZ();
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
}