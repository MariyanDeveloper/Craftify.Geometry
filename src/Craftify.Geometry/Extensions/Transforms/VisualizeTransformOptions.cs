using Autodesk.Revit.DB;

namespace Craftify.Geometry.Options;

public class VisualizeTransformOptions
{
    public int Scale { get; set; } = 3;
    public Color BasisXColor { get; set; } = new(255, 0, 0);
    public Color BasisYColor { get; set; } = new(0, 128, 0);
    public Color BasisZColor { get; set; } = new(70, 65, 240);
}