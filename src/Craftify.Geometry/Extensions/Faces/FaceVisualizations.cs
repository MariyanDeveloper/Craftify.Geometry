using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Faces;

public static class FaceVisualizations
{
    public static void VisualizeAsCurvesIn(this Face face, Document document)
    {
        if (face is null) throw new ArgumentNullException(nameof(face));
        if (document is null) throw new ArgumentNullException(nameof(document));
        face
            .GetEdgesAsCurveLoops()
            .SelectMany(x => x)
            .VisualizeIn(document);
    }
    public static void VisualizeAsCurvesIn(this IEnumerable<Face> faces, Document document)
    {
        if (faces is null) throw new ArgumentNullException(nameof(faces));
        if (document is null) throw new ArgumentNullException(nameof(document));
        foreach (var face in faces)
        {
            face.VisualizeAsCurvesIn(document);
        }
    }
}