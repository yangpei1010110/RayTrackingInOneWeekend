using System.Numerics;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Hit;

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

    public static Vector3 RayColor(Ray r, IHittable world)
    {
        HitRecord rec;
        if (world.Hit(r, 0f, float.MaxValue, out rec))
        {
            return 0.5f * (rec.Normal + new Vector3(1));
        }

        Vector3 unitDirection = Vector3.Normalize(r.Direction);
        float t = 0.5f * (unitDirection.Y + 1.0f);
        return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
    }
}