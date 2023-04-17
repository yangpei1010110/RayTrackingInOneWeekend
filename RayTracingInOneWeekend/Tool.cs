using System.Numerics;
using RayTracingInOneWeekend.Utility;

namespace RayTracingInOneWeekend;

public static class Tool
{
    public static Vector3 RayColor(Ray r)
    {
        Vector3 unitDirection = Vector3.Normalize(r.Direction);
        float   t             = 0.5f * (unitDirection.Y + 1.0f);
        return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
    }
}