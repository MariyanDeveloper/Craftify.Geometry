using Autodesk.Revit.DB;

namespace Craftify.Geometry.BoundingBoxes;

public static class BoundingBox
{
    public static BoundingBoxXYZ CreateEmptyXYZ()
    {
        return new BoundingBoxXYZ();
    }

    public static BoundingBoxXYZ Create(XYZ min, XYZ max) => new()
    {
        Min = min,
        Max = max
    };
    public static BoundingBoxXYZ Create(XYZ min, XYZ max, Transform transform) => new()
    {
        Min = min,
        Max = max,
        Transform = transform
    };
}