using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.BoundingBoxes.Builders;

public class SectionBoundingBoxBuilder
{
    private double _locationLength;
    private double _height;
    private double _farClipOffset;
    private XYZ _origin = XYZ.Zero;
    private XYZ _facingVector = XYZ.BasisY;

    public static SectionBoundingBoxBuilder Create() => new();

    public SectionBoundingBoxBuilder OfLocationLength(double locationLength)
    {
        _locationLength = locationLength;
        return this;
    }
    public SectionBoundingBoxBuilder OfHeight(double height)
    {
        _height = height;
        return this;
    }
    public SectionBoundingBoxBuilder OfFarClipOffset(double farClipOffset)
    {
        _farClipOffset = farClipOffset;
        return this;    
    }
    public SectionBoundingBoxBuilder AtOrigin(XYZ origin)
    {
        _origin = origin;
        return this;    
    }

    public SectionBoundingBoxBuilder DirectTo(XYZ facingVector)
    {
        _facingVector = facingVector;
        return this;
    }

    public BoundingBoxXYZ Build()
    {
        var transform = _facingVector
            .ToTransformAsYFacing()
            .CreateAdaptedToSection()
            .SetOrigin(_origin);
        var boundingBox = new BoundingBoxBuilder()
            .OfLength(_height)
            .OfWidth(_locationLength)
            .OfHeight(_farClipOffset)
            .WithTransform(transform)
            .Build()
            .Align(Side.Length, Alignment.Right);
        return boundingBox;
    }
}