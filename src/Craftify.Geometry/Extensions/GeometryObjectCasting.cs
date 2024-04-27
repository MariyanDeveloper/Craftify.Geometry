using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class GeometryObjectCasting
{
    public static T Cast<T>(this GeometryObject geometryObject)
        where T : GeometryObject => (T)geometryObject;
}