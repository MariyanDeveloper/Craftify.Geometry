using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions;

public static class GeometryInstanceExtensions
{
    public static IEnumerable<T> ExtractGeometries<T>(
        this GeometryInstance geometryInstance,
        GeometryRepresentation geometryRepresentation) where T : GeometryObject
    {
        var familyGeometryElement = (geometryRepresentation == GeometryRepresentation.Symbol)
            ? geometryInstance.SymbolGeometry
            : geometryInstance.GetInstanceGeometry();
        foreach (var geometryObject in familyGeometryElement.ExtractRootGeometries<T>(geometryRepresentation))
        {
            yield return geometryObject;
        }
    }
}