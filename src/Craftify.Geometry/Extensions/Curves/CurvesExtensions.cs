using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurvesExtensions
{
    public static ContinuousCurveCollection ToContinuous(this IEnumerable<Curve> curves)
    {
        var contiguousCurves = new List<Curve>();
        var remainingCurves = curves.ToList();

        if (remainingCurves.Count == 0)
        {
            return new ContinuousCurveCollection(contiguousCurves);
        }
        var currentCurve = remainingCurves[0];
        contiguousCurves.Add(currentCurve);
        remainingCurves.RemoveAt(0);

        while (remainingCurves.Count > 0)
        {
            var curveAdded = false;

            for (var i = 0; i < remainingCurves.Count; i++)
            {
                if (currentCurve.IsContinuousWith(remainingCurves[i]))
                {
                    currentCurve = remainingCurves[i];
                    contiguousCurves.Add(currentCurve);
                    remainingCurves.RemoveAt(i);
                    curveAdded = true;
                    break;
                }
                if (currentCurve.ShareSameEndPoints(remainingCurves[i]))
                {
                    currentCurve = remainingCurves[i].CreateReversed();
                    contiguousCurves.Add(currentCurve);
                    remainingCurves.RemoveAt(i);
                    curveAdded = true;
                    break;
                }
            }
            if (curveAdded is false)
            {
                break;
            }
        }
        return new ContinuousCurveCollection(contiguousCurves);
    }
}