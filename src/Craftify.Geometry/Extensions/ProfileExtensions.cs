using System.Linq;
using Autodesk.Revit.DB;
using Profile = Craftify.Geometry.Collections.Profile;

namespace Craftify.Geometry.Extensions;

public static class ProfileExtensions
{
    public static Profile CreateTransformed(this Profile profile, Transform transform)
    {
        return profile.Select(x =>
                CurveLoop.CreateViaTransform(x,
                    transform))
            .ToProfile();
    }
    
    public static Profile CreateMovedBy(this Profile profile, XYZ translation)
    {
        return profile.Select(x =>
                CurveLoop.CreateViaTransform(x,
                    Transform.CreateTranslation(translation)))
            .ToProfile();
    }
}