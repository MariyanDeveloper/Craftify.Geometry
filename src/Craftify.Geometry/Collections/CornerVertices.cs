using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class CornerVertices : List<XYZ>
{
    public XYZ Left { get; }
    public XYZ Right { get; }

    public CornerVertices(XYZ left, XYZ right) : base(new []{left, right})
    {
        Left = left;
        Right = right;
    }
}