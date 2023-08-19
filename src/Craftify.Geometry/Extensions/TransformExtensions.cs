using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.Extensions
{
    public static class TransformExtensions
    {
        public static Transform Clone(this Transform transform) => new Transform(transform);
        public static Transform CreateAdaptedToSection(this Transform transformToAdapt)
        {
            var outputTransform = Transform.Identity;
            outputTransform.BasisZ = transformToAdapt.BasisY;
            outputTransform.BasisY = transformToAdapt.BasisZ;
            outputTransform.BasisX = -transformToAdapt.BasisX;
            return outputTransform;
        }
        
        public static void VisualizeIn(
            this Transform transform,
            Document document,
            Action<VisualizeTransformOptions>? configOptions = null)
        {
            var options = new VisualizeTransformOptions();
            configOptions?.Invoke(options);
            
            var basisVectorColors = new List<Color>
            {
                options.BasisXColor,
                options.BasisYColor,
                options.BasisZColor,
            };
            var colorToLines = GetLineToColorMappings(
                transform,
                options.Scale,
                basisVectorColors);
            RenderLinesInDocument(document, colorToLines);
            transform.Origin.VisualizeIn(document);
        }
        
        private static void RenderLinesInDocument(Document document, IEnumerable<(Line Line, Color Color)> colorToLines)
        {
            foreach (var (line, color) in colorToLines)
            {
                var directShape = document.CreateDirectShape(
                    line);
                var overrideGraphics = new OverrideGraphicSettings();
                overrideGraphics.SetProjectionLineColor(color);
                overrideGraphics.SetProjectionLineWeight(4);
                document.ActiveView.SetElementOverrides(directShape.Id, overrideGraphics);
            }
        }

        private static IEnumerable<(Line Line, Color Color)> GetLineToColorMappings(
            Transform transform,
            int scale,
            List<Color> basisVectorColors)
        {
            var colorToLines = Enumerable.Range(0, 3)
                .Select(transform.get_Basis)
                .Select(x => Line.CreateBound(
                    transform.Origin,
                    transform.Origin + x * scale))
                .Zip(basisVectorColors, (line, color) => (Line: line, Color: color));
            return colorToLines;
        }
    }
}
