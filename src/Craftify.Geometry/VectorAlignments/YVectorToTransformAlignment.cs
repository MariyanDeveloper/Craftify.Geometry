using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.VectorAlignments;

public class YVectorToTransformAlignment : IVectorToTransformAlignment
{
    public Transform Align(XYZ vector)
    {
        if (vector is null) throw new ArgumentNullException(nameof(vector));
        var defaultVector = XYZ.BasisZ;
        if (defaultVector.IsAlmostEqualTo(vector))
        {
            return new TransformBuilder()
                .BuildYAsZ();
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