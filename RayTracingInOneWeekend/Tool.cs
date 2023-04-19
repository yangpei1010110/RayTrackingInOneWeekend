using System.Numerics;
using RayTracingInOneWeekend.Utility;

namespace RayTracingInOneWeekend;

public static class Tool
{
    public static float HitSphere(Vector3 center, float radius, Ray r)
    {
        Vector3 oc = r.Origin - center;
        float a = r.Direction.LengthSquared();
        float halfB = Vector3.Dot(oc, r.Direction);
        float c = oc.LengthSquared() - radius * radius;
        float discriminant = halfB * halfB - a * c;
        return discriminant < 0 ? -1.0f : (-halfB - MathF.Sqrt(discriminant)) / a;
    }

    public static Vector3 RayColor(Ray r)
    {
        Vector3 sphereCenter = new(0, 0, -1);
        float hitT = HitSphere(sphereCenter, 0.5f, r);
        if (hitT > 0.0f)
        {
            Vector3 n = Vector3.Normalize(r.At(hitT) - sphereCenter);
            return 0.5f * new Vector3(n.X + 1, n.Y + 1, n.Z + 1);
        }

        Vector3 unitDirection = Vector3.Normalize(r.Direction);
        float t = 0.5f * (unitDirection.Y + 1.0f);
        return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
    }
}