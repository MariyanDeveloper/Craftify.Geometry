using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.Extensions;

public static class OutlineExtensions
{
    public static void VisualizeIn(this Outline outline, Document document)
    {
        document.CreateDirectShape(new GeometryObject[]
        {
            outline.MinimumPoint.ToPoint(),
            outline.MaximumPoint.ToPoint(),
        });
    }
}