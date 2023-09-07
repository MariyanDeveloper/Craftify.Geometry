using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public class FurthermostResult
{
    public XYZ Left { get; }
    public XYZ Right { get; }

    public FurthermostResult(XYZ left, XYZ right)
    {
        Left = left;
        Right = right;
    }
}