using System;
using System.Xml;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions;

public static class SideExtensions
{
    public static XYZ GetCorrespondingVector(this Side side) => side switch
    {
        Side.Length => XYZ.BasisX,
        Side.Width => XYZ.BasisY,
        Side.Height => XYZ.BasisZ,
        _ => throw new InvalidOperationException($"Cannot use ${side.ToString()}")
    };
}