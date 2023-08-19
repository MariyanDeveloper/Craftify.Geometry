using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class Curves : List<Curve>
{
    public Curves()
    {
        
    }

    public Curves(IEnumerable<Curve> curves): base(curves)
    {
        
    }
}