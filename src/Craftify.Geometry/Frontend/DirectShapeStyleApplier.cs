using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

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