using System.Numerics;

namespace RayTracingInOneWeekend.Utility.Extensions;

public static class Vector3Extension
{
    public static bool NearZero(this Vector3 v) => MathF.Abs(v.X) < 1e-8 && MathF.Abs(v.Y) < 1e-8 && MathF.Abs(v.Z) < 1e-8;

    public static Vector3 Refract(Vector3 uv, Vector3 n, float etaiOverEtat)
    {
        float cosTheta = MathF.Min(Vector3.Dot(-uv, n), 1.0f);
        Vector3 rOutPerp = etaiOverEtat * (uv + cosTheta * n);
        Vector3 rOutParallel = -MathF.Sqrt(MathF.Abs(1.0f - rOutPerp.LengthSquared())) * n;
        return rOutPerp + rOutParallel;
    }
}