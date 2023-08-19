using Autodesk.Revit.DB;

namespace Craftify.Geometry.VectorAlignments;

public class TransformBuilder
{
    public Transform BuildXAsZ()
    {
        var transform = Transform.Identity;
        transform.BasisX = XYZ.BasisZ;
        transform.BasisY = XYZ.BasisY;
        transform.BasisZ = -XYZ.BasisX;
        return transform;
    }

    public Transform BuildYAsZ()
    {
        var transform = Transform.Identity;
        transform.BasisX = XYZ.BasisX;
        transform.BasisY = XYZ.BasisZ;
        transform.BasisZ = -XYZ.BasisY;
        return transform;
    }

    public Transform BuildZAsX()
    {
        var transform = Transform.Identity;
        transform.BasisX = -XYZ.BasisZ;
        transform.BasisY = XYZ.BasisY;
        transform.BasisZ = XYZ.BasisX;
        return transform;
    }
}