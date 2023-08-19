using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class DocumentExtensions
{
    public static DirectShape CreateDirectShape(
        this Document document,
        IEnumerable<GeometryObject> geometryObjects,
        BuiltInCategory builtInCategory = BuiltInCategory.OST_GenericModel)
    {
        var directShape = DirectShape.CreateElement(document, new ElementId(builtInCategory));
        directShape.SetShape(geometryObjects.ToList());
        return directShape;

    }

    public static DirectShape CreateDirectShape(
        this Document document,
        GeometryObject geometryObject,
        BuiltInCategory builtInCategory = BuiltInCategory.OST_GenericModel)
    {

        var directShape = DirectShape.CreateElement(document, new ElementId(builtInCategory));
        directShape.SetShape(new List<GeometryObject>() { geometryObject });
        return directShape;
    }
}