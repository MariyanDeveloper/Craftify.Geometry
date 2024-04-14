using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZVisualizations
{
    public static void VisualizeIn(
        this XYZ point, Document document)
    {
        document.CreateDirectShape(Point.Create(point));
    }
    public static void VisualizeIn(this IEnumerable<XYZ> points, Document document)
    {
        document.CreateDirectShape(points.Select(Point.Create));
    }
    
    public static void VisualizePerEachIn(this IEnumerable<XYZ> points, Document document)
    {
        foreach (var point in points)
        {
            point.VisualizeIn(document);
        }
    }
}