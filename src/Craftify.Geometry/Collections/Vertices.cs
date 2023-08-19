using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class Vertices : List<XYZ>
{
    public Vertices()
    {
        
    }
    public Vertices(IEnumerable<XYZ> vertices): base(vertices)
    {
        
    }
}