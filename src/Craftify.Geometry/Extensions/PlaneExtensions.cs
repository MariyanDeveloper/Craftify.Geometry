using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.Extensions
{
    public static class PlaneExtensions
    {
        public static Transform ToTransform(this Plane plane, Orientation orientation = Orientation.Facing) =>
            orientation switch
              {
                  Orientation.Facing => CreateFacingTransform(plane),
                  _ => CreateHandTransform(plane)
              };
        

        private static Transform CreateHandTransform(Plane plane)
        {
            var transform = Transform.Identity;
            transform.Origin = plane.Origin;
            transform.BasisX = plane.Normal;
            transform.BasisY = plane.XVec;
            transform.BasisZ = plane.YVec;
            return transform;
        }

        private static Transform CreateFacingTransform(Plane plane)
        {
            var transform = Transform.Identity;
            transform.Origin = plane.Origin;
            transform.BasisY = plane.Normal;
            transform.BasisX = plane.XVec;
            transform.BasisZ = plane.YVec;
            return transform;
        }

        public static void Visualize(
            this Plane plane, Document document, Action<PlaneVisualizeOptions>? configOptions = null)
        {
            var options = new PlaneVisualizeOptions();
            configOptions?.Invoke(options);
            var planeOrigin = plane.Origin;
            var scale = options.Scale;
            var upperRightCorner = planeOrigin + (plane.XVec * scale) + (plane.YVec * scale);
            var upperLeftCorner = planeOrigin - (plane.XVec * scale) + (plane.YVec * scale);
            var bottomRightCorner = planeOrigin + (plane.XVec * scale) - (plane.YVec * scale);
            var bottomLeftCorner = planeOrigin - (plane.XVec * scale) - (plane.YVec * scale);
            var curves = CreateCurves(
                plane,
                upperRightCorner,
                upperLeftCorner,
                bottomRightCorner,
                bottomLeftCorner);
            document.CreateDirectShape(curves);
        }

        private static List<GeometryObject> CreateCurves(Plane plane, XYZ upperRightCorner, XYZ upperLeftCorner, XYZ bottomRightCorner,
            XYZ bottomLeftCorner)
        {
            var curves = new List<GeometryObject>
            {
                Line.CreateBound(
                    upperRightCorner, upperLeftCorner),
                Line.CreateBound(
                    upperRightCorner, bottomRightCorner),
                Line.CreateBound(
                    upperLeftCorner, bottomLeftCorner),
                Line.CreateBound(
                    bottomLeftCorner, bottomRightCorner),
                Line.CreateBound(
                    plane.Origin, plane.Origin + plane.Normal)
            };
            return curves;
        }
    }
}
