using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry;

public class IdenticalXYZEqualityComparer : IEqualityComparer<XYZ>
{
    private readonly int _decimalPointCount = 4;
    private readonly int _randomPrimeNumber = 397;
    
    public bool Equals(XYZ fromPoint, XYZ toPoint)
    {
        if (ReferenceEquals(fromPoint, toPoint)) return true;
        if (ReferenceEquals(fromPoint, null)) return false;
        if (ReferenceEquals(toPoint, null)) return false;
        if (fromPoint.GetType() != toPoint.GetType()) return false;
        return fromPoint.IsAlmostEqualTo(toPoint);
    }
    public int GetHashCode(XYZ point)
    {
        unchecked
        {
            var hashCode = Math.Round(point.Z, _decimalPointCount).GetHashCode();
            hashCode = (hashCode * _randomPrimeNumber) ^ Math.Round(point.Y, _decimalPointCount).GetHashCode();
            hashCode = (hashCode * _randomPrimeNumber) ^ Math.Round(point.X, _decimalPointCount).GetHashCode();
            return hashCode;
        }
    }
}