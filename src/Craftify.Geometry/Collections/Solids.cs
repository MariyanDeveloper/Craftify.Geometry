using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class Solids : List<Solid>
{
    public Solids()
    {
        
    }

    public Solids(IEnumerable<Solid> solids): base(solids)
    {
        
    }
}