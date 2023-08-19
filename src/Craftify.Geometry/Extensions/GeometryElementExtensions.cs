using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions
{
    public static class GeometryElementExtensions
    {
        public static IEnumerable<T> ExtractRootGeometries<T>(
            this GeometryElement geometryElement,
            GeometryRepresentation geometryRepresentation = GeometryRepresentation.Instance)
            where T : GeometryObject
        {
            if (geometryElement is null) throw new ArgumentNullException(nameof(geometryElement));
            foreach (var geometryObject in geometryElement)
            {
                if (geometryObject is T ultimateElement)
                {
                    yield return ultimateElement;
                }
                if (geometryObject is GeometryInstance geometryInstance)
                {
                    foreach (var familyGeometryObject in geometryInstance.ExtractGeometries<T>(geometryRepresentation))
                    {
                        yield return familyGeometryObject;
                    }
                }
                if (geometryObject is GeometryElement nestedGeometryElement)
                {
                    foreach (var nestedElement in ExtractRootGeometries<T>(
                                 nestedGeometryElement, geometryRepresentation))
                    {
                        yield return nestedElement;
                    }
                }
            }
        }
    }
}
