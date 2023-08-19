using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class FaceExtensions
{
    public static void VisualizeIn(this Face face, Document document)
    {
        if (face is null) throw new ArgumentNullException(nameof(face));
        if (document is null) throw new ArgumentNullException(nameof(document));
        face
            .GetEdgesAsCurveLoops()
            .SelectMany(x => x)
            .VisualizeIn(document);
    }
    public static void VisualizeIn(this IEnumerable<Face> faces, Document document)
    {
        if (faces is null) throw new ArgumentNullException(nameof(faces));
        if (document is null) throw new ArgumentNullException(nameof(document));
        foreach (var face in faces)
        {
            face.VisualizeIn(document);
        }
    }
}