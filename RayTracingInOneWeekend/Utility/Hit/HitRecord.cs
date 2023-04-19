using System.Numerics;

namespace RayTracingInOneWeekend.Utility.Hit;

public struct HitRecord
{
    public Vector3 Point;
    public Vector3 Normal;
    public float   T;

    public HitRecord(Vector3 point, Vector3 normal, float t)
    {
        Point = point;
        Normal = normal;
        T = t;
    }
}