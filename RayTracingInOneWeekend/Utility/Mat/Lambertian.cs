using System.Numerics;
using RayTracingInOneWeekend.Utility.Extensions;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend.Utility.Mat;

public struct Lambertian : IMaterial
{
    public Lambertian(Vector3 albedo) => Albedo = albedo;

    public Vector3 Albedo { get; set; }

    public bool Scatter(Ray rIn, HitRecord rec, out Vector3 attenuation, out Ray scattered)
    {
        var scatterDirection = rec.Normal + Tool.RandomUnitVector();
        scatterDirection = scatterDirection.NearZero() ? rec.Normal : scatterDirection;
        scattered = new Ray(rec.Point, scatterDirection);
        attenuation = Albedo;
        return true;
    }
}