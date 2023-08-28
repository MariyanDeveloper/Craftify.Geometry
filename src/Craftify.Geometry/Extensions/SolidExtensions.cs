using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;

namespace Craftify.Geometry.Extensions
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

        public static Solids ToSolids(this IEnumerable<Solid> solids) => new Solids(solids);
        public static IEnumerable<T> GetChildComponents<T>(this Solid solid)
        {
            if (typeof(T) == typeof(Face))
            {
                return (IEnumerable<T>)solid.GetFaces();
            }
            if (typeof(T) == typeof(Curve))
            {
                return (IEnumerable<T>)solid.GetCurves();
            }

            if (typeof(T) == typeof(XYZ))
            {
                return (IEnumerable<T>)solid.GetVertices();
            }

            if (typeof(T) == typeof(CurveLoop))
            {
                return (IEnumerable<T>)solid.GetFaces()
                    .SelectMany(f => f.GetEdgesAsCurveLoops());
            }

            throw new NotImplementedException($"Given type : {typeof(T)} is not supported");
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
        
        public static Faces GetFaces(
            this Solid solid)
        {
            return new Faces(solid.Faces.OfType<Face>());

        }
        public static Curves GetCurves(
            this Solid solid)
        {
            return new Curves(solid.GetFaces()
                .SelectMany(x => x.GetEdgesAsCurveLoops())
                .SelectMany(x => x));
        }
        
        public static Vertices GetVertices(
            this Solid solid)
        {
            return new Vertices(solid.GetCurves()
                .SelectMany(x => x.Tessellate()));
        }

    }
}
