using Autodesk.Revit.DB;

namespace Craftify.Geometry.Frontend.VisualizationComponentFeature;

public record ComponentStyle(
    string PatternName,
    Color Color
);