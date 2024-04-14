using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class TransformReturns
{
    public static Transform CreateXAsZ()
    {
        var transform = Transform.Identity;
        transform.BasisX = XYZ.BasisZ;
        transform.BasisY = XYZ.BasisY;
        transform.BasisZ = -XYZ.BasisX;
        return transform;
    }
    
    public static Transform CreateYAsZ()
    {
        var transform = Transform.Identity;
        transform.BasisX = XYZ.BasisX;
        transform.BasisY = XYZ.BasisZ;
        transform.BasisZ = -XYZ.BasisY;
        return transform;
    }
    
    public static Transform CreateZAsX()
    {
        var transform = Transform.Identity;
        transform.BasisX = -XYZ.BasisZ;
        transform.BasisY = XYZ.BasisY;
        transform.BasisZ = XYZ.BasisX;
        return transform;
    }
}