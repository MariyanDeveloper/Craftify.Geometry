using System.Collections.Generic;
using Autodesk.Revit.DB;
using Profile = Craftify.Geometry.Collections.Profile;

namespace Craftify.Geometry.Extensions;

public static class CurveLoopsExtensions
{
    public static Profile ToProfile(this IEnumerable<CurveLoop> curveLoops) => new(curveLoops);
}