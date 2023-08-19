using Autodesk.Revit.DB;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.VectorAlignments;

public class ZVectorToTransformAlignment : IVectorToTransformAlignment
{
    public Transform Align(XYZ vector)
    {
        var zAxis = vector.Normalize();
        var xAxis = new XYZ(1, 0, 0).CrossProduct(zAxis).Normalize();
        var yAxis = zAxis.CrossProduct(xAxis).Normalize();
        var transform = Transform.Identity;
        transform.BasisX = xAxis;
        transform.BasisY = yAxis;
        transform.BasisZ = zAxis;
        return transform;
    }
}