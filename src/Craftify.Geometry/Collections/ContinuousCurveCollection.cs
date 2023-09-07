using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Collections;

public class ContinuousCurveCollection : List<Curve>
{
    public ContinuousCurveCollection() { }
    public ContinuousCurveCollection(IEnumerable<Curve> curves) : base(curves)
    {
        
    }
}