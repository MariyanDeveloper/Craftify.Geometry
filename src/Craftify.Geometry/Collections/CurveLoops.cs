using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class CurveLoops : List<CurveLoop>
{
    public CurveLoops()
    {
        
    }
    public CurveLoops(IEnumerable<CurveLoop> curveLoops) : base(curveLoops)
    {
        
    }
}