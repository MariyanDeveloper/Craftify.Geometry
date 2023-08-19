using System.Collections.Generic;

namespace Craftify.Geometry.Collections;

public class Coordinates : List<double>
{
    public Coordinates(IEnumerable<double> coordinates): base(coordinates)
    {
        
    }
}