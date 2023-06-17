using Autodesk.Revit.DB;

namespace Craftify.Geometry;

public static class GeometryObjectExtensions
{
    public static void VisualizeIn(this GeometryObject geometryObject, Document document)
    {
        document.CreateDirectShape(geometryObject);
    }
}