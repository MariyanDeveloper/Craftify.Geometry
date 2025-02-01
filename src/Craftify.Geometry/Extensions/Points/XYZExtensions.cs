using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZExtensions
{
    public static XYZ ToVector(
        this XYZ firstPoint, XYZ secondPoint)
    {
        return (secondPoint - firstPoint);
    }
    
    public static XYZ ToNormalizedVectorTo(
        this XYZ firstPoint, XYZ secondPoint)
    {
        return (secondPoint - firstPoint).Normalize();
    }

}