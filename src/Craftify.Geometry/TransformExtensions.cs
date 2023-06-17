using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Craftify.Geometry
{
    public static class TransformExtensions
    {
        public static void Visualize(
            this Transform transform, Document document, double scale = 3)
        {
            var colors = new List<Color>
            {
                new Color(255, 0, 0),
                new Color(0, 128, 0),
                new Color(70, 65, 240)

            };
            var colorToLines = Enumerable.Range(0, 3)
                .Select(transform.get_Basis)
                .Select(x => Line.CreateBound(
                    transform.Origin,
                    transform.Origin + x * scale))
                .Zip(colors, (line, color) => (Line: line, Color: color))
                .ToList();
            foreach (var (line, color) in colorToLines)
            {
                var directShape = document.CreateDirectShape(
                    line);
                var overrideGraphics = new OverrideGraphicSettings();
                overrideGraphics.SetProjectionLineColor(color);
                overrideGraphics.SetProjectionLineWeight(4);
                document.ActiveView.SetElementOverrides(directShape.Id, overrideGraphics);

            }
            transform.Origin.VisualizeIn(document);

        }


    }
}
