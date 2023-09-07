using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves;

public class FurthestCurvesResult
{
    public Curve FirstCurve { get; }
    public Curve SecondCurve { get; }

    public FurthestCurvesResult(Curve firstCurve, Curve secondCurve)
    {
        FirstCurve = firstCurve;
        SecondCurve = secondCurve;
    }
}