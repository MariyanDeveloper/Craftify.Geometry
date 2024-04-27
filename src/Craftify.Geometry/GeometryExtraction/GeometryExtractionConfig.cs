using Autodesk.Revit.DB;
using Craftify.Functional;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.GeometryExtraction;

public class GeometryExtractionConfig
{
    public static readonly GeometryExtractionConfig Default = new();
    public Option<Transform> Transform { get; init; } = F.None;
    public GeometryRepresentation GeometryRepresentation { get; init; } = GeometryRepresentation.Instance;
}