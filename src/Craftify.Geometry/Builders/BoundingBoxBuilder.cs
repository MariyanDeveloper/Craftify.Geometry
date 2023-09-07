using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.BoundingBoxes;

namespace Craftify.Geometry.Builders;

public class BoundingBoxBuilder 
{
    private SideSpecification _lengthSpecification = new(1, Alignment.Center);
    private SideSpecification _widthSpecification = new(1, Alignment.Center);
    private SideSpecification _heightSpecification = new (1, Alignment.Bottom);
    private XYZ? _origin;
    private Transform? _orientationTransform;
    private Transform? _transform;
    
    public BoundingBoxBuilder OfLength(double length, Alignment alignment = Alignment.Center)
    {
        _lengthSpecification = new SideSpecification(length, alignment);
        return this;
    }
    public BoundingBoxBuilder OfWidth(double width, Alignment alignment = Alignment.Center)
    {
        _widthSpecification = new SideSpecification(width, alignment);
        return this;
    }
    public BoundingBoxBuilder OfHeight(double height, Alignment alignment = Alignment.Bottom)
    {
        _heightSpecification = new SideSpecification(height, alignment);
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
        var boundingBox = new BoundingBoxXYZ();
        boundingBox.SetLength(_lengthSpecification.Value, _lengthSpecification.Alignment);
        boundingBox.SetWidth(_widthSpecification.Value, _widthSpecification.Alignment);
        boundingBox.SetHeight(_heightSpecification.Value, _heightSpecification.Alignment);
        if (_transform is not null)
        {
            boundingBox.Transform = _transform;
            return boundingBox;
        }
        boundingBox.ChangePlacement(_origin ?? XYZ.Zero, _orientationTransform ?? Transform.Identity);
        return boundingBox;
    }

}