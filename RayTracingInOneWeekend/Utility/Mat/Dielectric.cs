using System.Numerics;
using RayTracingInOneWeekend.Utility.Extensions;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend.Utility.Mat;

public struct Dielectric : IMaterial
{
    public Dielectric(float refIdx) => RefIdx = refIdx;

    public float RefIdx { get; set; }

    public bool Scatter(Ray rIn, HitRecord rec, out Vector3 attenuation, out Ray scattered)
    {
        attenuation = new Vector3(1, 1, 1);
        float refractionRatio = rec.FrontFace ? (1 / RefIdx) : RefIdx;
        Vector3 unitDirection = Vector3.Normalize(rIn.Direction);
        float cosTheta = MathF.Min(Vector3.Dot(-unitDirection, rec.Normal), 1f);
        float sinTheta = MathF.Sqrt(1f - cosTheta * cosTheta);
        bool cannotRefract = refractionRatio * sinTheta > 1f;
        Vector3 direction = cannotRefract
                         || Reflectance(cosTheta, refractionRatio) > Random.Shared.NextSingle()
            ? Vector3.Reflect(unitDirection, rec.Normal)
            : Vector3Extension.Refract(unitDirection, rec.Normal, refractionRatio);
        scattered = new Ray(rec.Point, direction);
        return true;
    }

    private float Reflectance(float cosine, float refIdx)
    {
        float r0 = (1 - refIdx) / (1 + refIdx);
        r0 *= r0;
        return r0 + (1 - r0) * MathF.Pow(1 - cosine, 5);
    }
}