using System.Numerics;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend.Utility.Mat;

public struct Metal : IMaterial
{
    public Metal(Vector3 albedo, float fuzz)
    {
        Albedo = albedo;
        Fuzz = MathF.Min(fuzz, 1);
    }

    public Vector3 Albedo { get; set; }
    public float   Fuzz   { get; set; }

    public bool Scatter(Ray rIn, HitRecord rec, out Vector3 attenuation, out Ray scattered)
    {
        var reflected = Vector3.Reflect(rIn.Direction, rec.Normal);
        scattered = new Ray(rec.Point, reflected + Fuzz * Tool.RandomInUnitSphere());
        attenuation = Albedo;
        return Vector3.Dot(scattered.Direction, rec.Normal) > 0;
    }
}