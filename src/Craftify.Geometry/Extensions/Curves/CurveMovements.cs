using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveMovements
{
    public static T MoveBy<T>(
        this T curve, XYZ vector) where T : Curve
    {
        return curve.CreateTransformed(
            Transform.CreateTranslation(vector)
        ).Cast<T>();
    }
    
    public static T MoveUpwards<T>(
        this T curve, double value) where T : Curve
    {
        return curve.MoveBy(
            XYZ.BasisZ * value
        );
    }
}