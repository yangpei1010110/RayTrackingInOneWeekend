using System.Numerics;

namespace RayTracingInOneWeekend.Utility.Extensions;

public static class Vector3Extension
{
    public static bool NearZero(this Vector3 v) => MathF.Abs(v.X) < 1e-8 && MathF.Abs(v.Y) < 1e-8 && MathF.Abs(v.Z) < 1e-8;
}