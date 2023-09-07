using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.Curves;

namespace Craftify.Geometry.Collections;

public class Profile : List<CurveLoop>, IEquatable<Profile>
{
    public Profile(IEnumerable<CurveLoop> curveLoops): base(curveLoops) { }
    public bool Equals(Profile? other)
    {
        if (other is null)
        {
            return false;
        }
        var flattenCurrentCurves = this.SelectMany(x => x);
        var flattenOtherCurves = other.SelectMany(x => x);
        return flattenCurrentCurves
            .All(expected => flattenOtherCurves.Any(expected.CompletelyInside));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }
        return Equals((Profile)obj);
    }
    public static bool operator ==(Profile? left, Profile? right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Profile? left, Profile? right)
    {
        return !(left == right);
    }
    public override int GetHashCode()
    {
        //TODO come up with a meaningful hash code
        return 1;
    }
}