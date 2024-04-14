using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZSelectors
{
    public static XYZ SelectMinByCoordinates(
        this IEnumerable<XYZ> points)
    {
        var pointsAsArray = points.ToArray();
        var minPoint = new XYZ(
            pointsAsArray.Min(x => x.X),
            pointsAsArray.Min(x => x.Y),
            pointsAsArray.Min(x => x.Z));
        return minPoint;
    }

    public static XYZ SelectMaxByCoordinates(
        this IEnumerable<XYZ> points)
    {
        var pointsAsArray = points.ToArray();
        var minPoint = new XYZ(
            pointsAsArray.Max(x => x.X),
            pointsAsArray.Max(x => x.Y),
            pointsAsArray.Max(x => x.Z));
        return minPoint;
    }

    public static XYZ WithZ(this XYZ point, double zCoordinate)
    {
        return new XYZ(
            point.X,
            point.Y,
            zCoordinate);
    }
    public static XYZ WithY(this XYZ point, double yCoordinate)
    {
        return new XYZ(
            point.X,
            yCoordinate,
            point.Z);
    }
    public static XYZ WithX(this XYZ point, double xCoordinate)
    {
        return new XYZ(
            xCoordinate,
            point.Y,
            point.Z);
    }
}