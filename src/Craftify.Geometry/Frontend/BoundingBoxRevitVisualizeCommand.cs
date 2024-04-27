using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions.Curves;
using Craftify.Geometry.Extensions.Points;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

// public record GeometryStyle();
// public record GeometryVisualizeComponent(
//     GeometryObject GeometryObject,
//     GeometryStyle GeometryStyle
//     );

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class BoundingBoxRevitVisualizeCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        var id = 350379;
        var view = (View)document.GetElement(new ElementId(id));
        var box = view.CropBox;
        var resolver = new SolidVisualizationComponentProvider();
        var geometry = resolver.Resolve(box, ApplyTransform.Yes);
        var patterns = new FilteredElementCollector(document)
            .OfClass(typeof(FillPatternElement))
            .ToList()
            .Cast<FillPatternElement>()
            .First(x => x.Name == Patterns.SolidFill);
        var patternId = patterns.Id;
        // foreach (var pattern in patterns.Where(x => x == Patterns.SolidFill))
        // {
        //     Console.WriteLine(pattern);
        // }
        using (var transaction = new Transaction(document, "Name"))
        {
            transaction.Start();
            // var geometryComponent = geometry.Cast<VisualizationComponent>();
            // var geometryObject = geometryComponent.GeometryObject;
            // var optionalStyle = geometryComponent.GeometryStyle;
            // optionalStyle
            // var overrideGraphics = new OverrideGraphicSettings();
            // overrideGraphics.SetSurfaceBackgroundPatternId(patternId);
            // overrideGraphics.SetSurfaceBackgroundPatternColor()
            // document.CreateDirectShape(geometryObject);
            transaction.Commit();
        }

        IEnumerable<Solid> solids = new List<Solid>();
        IEnumerable<Element> revitElements = new List<Element>();
        Solid targetSolid = default;
        XYZ vectorToMeasureBy = XYZ.Zero;
        var a = solids
            .SelectFurthermostPair(
                (s1, s2) => s1.ComputeCentroid()
                    .MeasureDistanceAlongVector(s2.ComputeCentroid(), vectorToMeasureBy));
        // targetSolid.SelectFurthermostElement(
        //     revitElements,
        //     (s, e) =>
        //     {
        //         
        //     })
        
        
        return Result.Succeeded;
    }
}