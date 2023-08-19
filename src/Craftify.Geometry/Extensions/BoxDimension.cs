namespace Craftify.Geometry.Extensions;

public class BoxDimension
{
    public double Length { get; }
    public double Width { get; }
    public double Height { get; }

    public BoxDimension(double length, double width, double height)
    {
        Length = length;
        Width = width;
        Height = height;
    }
}