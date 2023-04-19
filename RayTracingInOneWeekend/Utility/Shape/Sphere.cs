using System.Numerics;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend.Utility.Shape;

public class Sphere : IHittable
{
    public Sphere(Vector3 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public Vector3 Center { get; set; }
    public float   Radius { get; set; }

    public bool Hit(Ray r, float tMin, float tMax, out HitRecord rec)
    {
        rec = default(HitRecord);

        Vector3 oc = r.Origin - Center;
        float a = r.Direction.LengthSquared();
        float halfB = Vector3.Dot(oc, r.Direction);
        float c = oc.LengthSquared() - Radius * Radius;
        float discriminant = halfB * halfB - a * c;

        if (discriminant < 0)
        {
            return false;
        }

        float sqrtD = MathF.Sqrt(discriminant);
        float root = (-halfB - sqrtD) / a;
        if (root < tMin || tMax < root)
        {
            root = (-halfB + sqrtD) / a;
            if (root < tMin || tMax < root)
            {
                return false;
            }
        }

        rec.T = root;
        rec.Point = r.At(rec.T);
        rec.Normal = (rec.Point - Center) / Radius;

        return true;
    }
}