using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Functional;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.Curves;
using Craftify.Geometry.Extensions.Points;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

public interface IDirectShapeStyleApplier
{
    DirectShape Apply(DirectShape directShape, IGeometryStyle geometryStyle);
}

public interface IReadPatternIdRepository
{
    ElementId GetByName(string patternName);
}

public interface IElementGraphicsOverrider
{
    void Override(ElementId id, OverrideGraphicSettings overrideGraphicSettings);
}
public class DirectShapeStyleApplier : IDirectShapeStyleApplier
{
    private readonly IElementGraphicsOverrider _elementGraphicsOverrider;
    private readonly IReadPatternIdRepository _readPatternIdRepository;

    public DirectShapeStyleApplier(
        IElementGraphicsOverrider elementGraphicsOverrider,
        IReadPatternIdRepository readPatternIdRepository)
    {
        _elementGraphicsOverrider = elementGraphicsOverrider ?? throw new ArgumentNullException(nameof(elementGraphicsOverrider));
        _readPatternIdRepository = readPatternIdRepository ?? throw new ArgumentNullException(nameof(readPatternIdRepository));
    }
    public DirectShape Apply(DirectShape directShape, IGeometryStyle geometryStyle)
    {
        var surfacePatterns = geometryStyle.SurfacePatterns;
        var backgroundStyle = surfacePatterns.Background;
        var overrideGraphicSettings = MapStyleToOverrideSettings(backgroundStyle);
        _elementGraphicsOverrider.Override(directShape.Id, overrideGraphicSettings);
        return directShape;
    }

    private OverrideGraphicSettings MapStyleToOverrideSettings(ComponentStyle backgroundStyle)
    {
        var backgroundPatternName = backgroundStyle.PatternName;
        var patternId = _readPatternIdRepository
            .GetByName(backgroundPatternName);
        
        //this is purely Revit specific objects
        var overrideGraphicSettings = new OverrideGraphicSettings();
        overrideGraphicSettings.SetSurfaceBackgroundPatternId(patternId);
        overrideGraphicSettings.SetSurfaceBackgroundPatternColor(backgroundStyle.Color);
        return overrideGraphicSettings;
    }
}

public interface IBoundingBoxRenderer
{
    void Render(
        BoundingBoxXYZ boundingBox,
        IBoxVisualizationComponentProvider boxVisualizationComponentProvider,
        ApplyTransform applyTransform);
}


public sealed record PatternNotFoundError(string Message) : Error(Message);

public static class OverrideGraphicsProviderErrors
{
    public static Error PatternNotFoundError(string patternName)
    {
        return new PatternNotFoundError($"Pattern named {patternName} was not found in the database");
    }
}
public interface IOverrideGraphicsByStyleProvider
{
    Validation<OverrideGraphicSettings> Get(IGeometryStyle style);
}

public interface IElementVisualizer
{
    void VisualizeApplyingStyle(
        IVisualizationComponent visualizationComponent,
        IGeometryStyle geometryStyle);
}
public class RevitBoundingBoxRenderer : IBoundingBoxRenderer
{
    private readonly IOverrideGraphicsByStyleProvider _overrideGraphicsByStyleProvider;
    private readonly Document _document;

    public RevitBoundingBoxRenderer(
        IOverrideGraphicsByStyleProvider overrideGraphicsByStyleProvider,
        Document document)
    {
        _overrideGraphicsByStyleProvider = overrideGraphicsByStyleProvider ?? throw new ArgumentNullException(nameof(overrideGraphicsByStyleProvider));
        _document = document ?? throw new ArgumentNullException(nameof(document));
    }
    public void Render(
        BoundingBoxXYZ boundingBox,
        IBoxVisualizationComponentProvider boxVisualizationComponentProvider,
        ApplyTransform applyTransform)
    {
        var boxVisualizationComponent = boxVisualizationComponentProvider
            .Resolve(boundingBox, applyTransform);
        // boxVisualizationComponent.Match(
        //     x =>
        //     {
        //         var directShape = _document.CreateDirectShape(x.GeometryObject);
        //         x.GeometryStyle.Match(
        //             () => directShape,
        //             style =>
        //             {
        //                 var overrideGraphicSettings = new OverrideGraphicSettings();
        //
        //             })
        //         
        //     })
        throw new NotImplementedException();
    }
}

// public record GeometryStyle();
// public record GeometryVisualizeComponent(
//     GeometryObject GeometryObject,
//     GeometryStyle GeometryStyle
//     );



public static class Patterns
{
    public const string SolidFill = "<Solid fill>";
}

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