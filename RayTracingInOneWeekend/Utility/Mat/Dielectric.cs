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
        Vector3 refracted = Vector3Extension.Refract(unitDirection, rec.Normal, refractionRatio);
        scattered = new Ray(rec.Point, refracted);
        return true;
    }
}