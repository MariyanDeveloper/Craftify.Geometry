using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

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