using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class Faces : List<Face>
{
    public Faces()
    {
        
    }

    public Faces(IEnumerable<Face> faces): base(faces)
    {
        
    }
}