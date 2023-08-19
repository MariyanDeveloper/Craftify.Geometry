using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class GeometryObjectExtensions
{
    public static void VisualizeIn(this GeometryObject geometryObject, Document document)
    {
        document.CreateDirectShape(geometryObject);
    }
    public static void VisualizeIn(this IEnumerable<GeometryObject> geometries, Document document)
    {
        document.CreateDirectShape(geometries);
    }
}