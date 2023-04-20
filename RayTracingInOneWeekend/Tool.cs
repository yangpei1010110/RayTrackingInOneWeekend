using System.Numerics;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Extensions;
using RayTracingInOneWeekend.Utility.Hit;
using RayTracingInOneWeekend.Utility.Shape;

namespace RayTracingInOneWeekend;

public static class Tool
{
    public static Vector3 RandomVector3()
    {
        return new Vector3(Random.Shared.NextSingle(), Random.Shared.NextSingle(), Random.Shared.NextSingle());
    }

    public static Vector3 RandomVector3(float min, float max)
    {
        return new Vector3(Random.Shared.NextFloat(min, max), Random.Shared.NextFloat(min, max), Random.Shared.NextFloat(min, max));
    }

    public static Vector3 RayColor(Ray r, IHittable world, int depth)
    {
        HitRecord rec;
        if (depth <= 0)
        {
            return Vector3.Zero;
        }

        if (world.Hit(r, 0.001f, float.MaxValue, out rec))
        {
            Vector3 target = rec.Point + rec.Normal + Sphere.RandomInUnitSphere();
            return 0.5f * RayColor(new Ray(rec.Point, target - rec.Point), world, depth - 1);
        }

        Vector3 unitDirection = Vector3.Normalize(r.Direction);
        float t = 0.5f * (unitDirection.Y + 1.0f);
        return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
    }
}