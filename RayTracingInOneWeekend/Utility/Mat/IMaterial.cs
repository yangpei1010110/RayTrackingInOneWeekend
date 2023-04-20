using System.Numerics;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend.Utility.Mat;

public interface IMaterial
{
    public bool Scatter(Ray rIn, HitRecord rec, out Vector3 attenuation, out Ray scattered);
}