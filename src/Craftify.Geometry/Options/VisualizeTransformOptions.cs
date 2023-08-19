using Autodesk.Revit.DB;

namespace Craftify.Geometry.Options;

public class VisualizeTransformOptions
{
    public int Scale { get; set; } = 3;
    public Color BasisXColor { get; set; } = new Color(255, 0, 0);
    public Color BasisYColor { get; set; } = new Color(0, 128, 0);
    public Color BasisZColor { get; set; } = new Color(70, 65, 240);
}