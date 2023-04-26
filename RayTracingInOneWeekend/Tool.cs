using System.Numerics;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend;

public static class Tool
{
    public static Vector3 RandomVector3()
    {
        return new Vector3(RandomTool.NextFloat(), RandomTool.NextFloat(), RandomTool.NextFloat());
    }

    public static Vector3 RandomVector3(float min, float max)
    {
        return new Vector3(RandomTool.NextFloat(min, max), RandomTool.NextFloat(min, max), RandomTool.NextFloat(min, max));
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
            if (rec.Material.Scatter(r, rec, out Vector3 attenuation, out Ray scattered))
            {
                return attenuation * RayColor(scattered, world, depth - 1);
            }
            else
            {
                return Vector3.Zero;
            }
            // Vector3 target = rec.Point + rec.Normal + RandomUnitVector();
            // return 0.5f * RayColor(new Ray(rec.Point, target - rec.Point), world, depth - 1);
        }

        Vector3 unitDirection = Vector3.Normalize(r.Direction);
        float t = 0.5f * (unitDirection.Y + 1.0f);
        return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
    }

    public static Vector3 RandomInUnitSphere()
    {
        Vector3 p;
        do
        {
            p = Tool.RandomVector3(-1f, 1f);
        } while (p.LengthSquared() >= 1.0f);

        return p;
    }

    public static Vector3 HemisphereRandom(Vector3 normal)
    {
        Vector3 inUnitSphere = RandomInUnitSphere();
        return Vector3.Dot(inUnitSphere, normal) > 0.0f ? inUnitSphere : -inUnitSphere;
    }

    public static Vector3 RandomUnitVector()
    {
        return Vector3.Normalize(RandomInUnitSphere());
    }
}