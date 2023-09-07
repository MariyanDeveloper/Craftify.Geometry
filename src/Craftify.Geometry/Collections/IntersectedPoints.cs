using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class IntersectedPoints : List<XYZ>
{
    public IntersectedPoints() { }
    public IntersectedPoints(IEnumerable<XYZ> points): base(points)
    {
        
    }
}