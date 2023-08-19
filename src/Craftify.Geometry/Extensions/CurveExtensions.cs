using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions
{
    public static class CurveExtensions
    {
        private static readonly int _startIndex = 0;
        private static readonly double _centerParameter = 0.5;
        private static readonly int _endIndex = 1;
        
        /// <summary>
        /// This method is used to get normalized vector by curve
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static XYZ GetNormalizedVector(
            this Curve curve)
        {
            return curve.CreateVector().Normalize();
        }

        /// <summary>
        /// This method is used to get vector by curve
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static XYZ CreateVector(this Curve curve) =>
            curve.GetEndPoint(_endIndex) - curve.GetEndPoint(_startIndex);

        public static Vertices GetVertices(this Curve curve) => new Vertices(curve.Tessellate());

        public static void Extend(
            this Curve curve, double value, Extension extension = Extension.Both)
        {
            var startParameter = curve.GetEndParameter(_startIndex);
            var endParameter = curve.GetEndParameter(_endIndex);
            //TODO - check for more convenient switch expression
            if (extension == Extension.Both)
            {
                curve.MakeBound(startParameter - value, endParameter + value);
                return;
            }
            if (extension == Extension.Start)
            {
                curve.MakeBound(startParameter - value, endParameter);
                return;
            }
            curve.MakeBound(startParameter, endParameter + value);
        }
        public static Curve ExtendAsCloned(
            this Curve curve, double value, Extension extension = Extension.Both)
        {
            var clonedCurve = curve.Clone();
            clonedCurve.Extend(value, extension);
            return clonedCurve;
        }
        public static XYZ GetCenter(this Curve curve) => curve.Evaluate(_centerParameter, true);
        public static XYZ GetStartPoint(this Curve curve) => curve.GetEndPoint(_startIndex);
        public static XYZ GetEndPoint(this Curve curve) => curve.GetEndPoint(_endIndex);
    }
}
