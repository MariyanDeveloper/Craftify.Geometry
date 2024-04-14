using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.BoundingBoxes.Builders;

public class BoundingBoxBuilder
{
    private (double Height, Alignment Alignment) _heightSpecification = (1, Alignment.Bottom);
    private (double Length, Alignment Alignment) _lengthSpecification = (1, Alignment.Center);
    private Transform? _orientationTransform;
    private XYZ? _origin;
    private Transform? _transform;
    private (double Width, Alignment Alignment) _widthSpecification = (1, Alignment.Center);

    public static BoundingBoxBuilder Create()
    {
        return new BoundingBoxBuilder();
    }

    public BoundingBoxBuilder OfLength(double length, Alignment alignment = Alignment.Center)
    {
        _lengthSpecification = (length, alignment);
        return this;
    }

    public BoundingBoxBuilder OfWidth(double width, Alignment alignment = Alignment.Center)
    {
        _widthSpecification = (width, alignment);
        return this;
    }

    public BoundingBoxBuilder OfHeight(double height, Alignment alignment = Alignment.Bottom)
    {
        _heightSpecification = (height, alignment);
        return this;
    }

    public BoundingBoxBuilder AtOrigin(XYZ origin)
    {
        _origin = origin;
        return this;
    }

    public BoundingBoxBuilder WithOrientation(Transform transform)
    {
        _orientationTransform = transform;
        return this;
    }


    public BoundingBoxBuilder WithTransform(Transform transform)
    {
        _transform = transform;
        return this;
    }

    public BoundingBoxXYZ Build()
    {
        var boundingBox = BoundingBox.CreateEmptyXYZ()
            .SetLength(_lengthSpecification.Length, _lengthSpecification.Alignment)
            .SetWidth(_widthSpecification.Width, _widthSpecification.Alignment)
            .SetHeight(_heightSpecification.Height, _heightSpecification.Alignment);
        if (_transform is not null)
        {
            return boundingBox.SetTransform(_transform);
        }

        return boundingBox
            .ChangePlacement(_origin ?? XYZ.Zero, _orientationTransform ?? Transform.Identity);
    }
}