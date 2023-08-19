using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public ref struct AlignmentResult
{
    public XYZ RotationAxis { get; }
    public double Angle { get; }

    public AlignmentResult(XYZ rotationAxis, double angle)
    {
        RotationAxis = rotationAxis;
        Angle = angle;
    }
}