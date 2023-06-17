using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry
{
    public static class SolidExtensions
    {
        public static Solid Union(
            this IEnumerable<Solid> solids)
        {
            return solids
                .Where(x => x.HasVolume())
                .Aggregate((x, y) => BooleanOperationsUtils.ExecuteBooleanOperation(
                        x, y, BooleanOperationsType.Union));
        }
        
        public static Solid CreateMoved(this Solid solid, XYZ translation)
        {
            return solid.CreateTransformed(Transform.CreateTranslation(translation));
        }

        public static Solid CreateTransformed(this Solid solid, params Transform[] transforms)
        {
            var combinedTransform = transforms
                .Reverse()
                .Aggregate((current, next) => current.Multiply(next));
            return solid.CreateTransformed(combinedTransform);
        }
        public static Solid CreateTransformed(
            this Solid solid, Transform transform)
        {
            return SolidUtils.CreateTransformed(solid, transform);
        }

        public static bool HasVolume(
            this Solid solid) => solid.Volume > 0;

        public static bool HasFaces(
            this Solid solid) => solid.Faces.Size > 0;
        
        public static IEnumerable<Face> GetFaces(
            this Solid solid)
        {
            return solid.Faces.OfType<Face>();

        }
        public static IEnumerable<TComponent> GetComponents<TComponent>(this Solid solid)
        {
            var requiredType = typeof(TComponent);
            if (requiredType == typeof(Face))
            {
                return solid.GetFaces().Cast<TComponent>();
            }
            if (requiredType == typeof(Curve))
            {
                return solid.GetCurves().Cast<TComponent>();
            }
            if (requiredType == typeof(XYZ))
            {
                return solid.GetVertices().Cast<TComponent>();
            }
            throw new NotImplementedException($"The given type {requiredType} is not supported");
        }
        public static IEnumerable<Curve> GetCurves(
            this Solid solid)
        {
            return solid.GetFaces()
                .SelectMany(x => x.GetEdgesAsCurveLoops())
                .SelectMany(x => x);
        }
        
        public static IEnumerable<XYZ> GetVertices(
            this Solid solid)
        {
            return solid.GetCurves()
                .SelectMany(x => x.Tessellate());
        }

    }
}
