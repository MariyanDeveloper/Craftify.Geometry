using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.GeometryExtraction;

public class GeometryExtractionConfigBuilder
{
    private Transform? _transform;
    private bool _useSymbol = false;

    public static GeometryExtractionConfigBuilder Create() => new(); 

    public GeometryExtractionConfigBuilder ApplyTransform(Transform transform)
    {
        _transform = transform;
        return this;
    }

    public GeometryExtractionConfigBuilder UseSymbolRepresentation()
    {
        _useSymbol = true;
        return this;
    }
    
    public GeometryExtractionConfigBuilder UseInstanceRepresentation()
    {
        _useSymbol = false;
        return this;
    }

    public GeometryExtractionConfig Build()
    {
        var geometryRepresentation = _useSymbol ?
            GeometryRepresentation.Symbol
            : GeometryRepresentation.Instance;
        return new GeometryExtractionConfig()
        {
            Transform = _transform,
            GeometryRepresentation = geometryRepresentation
        };
    }
}