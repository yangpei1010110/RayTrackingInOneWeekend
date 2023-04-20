using System.Numerics;

namespace RayTracingInOneWeekend.Utility;

public struct Ray
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Vector3 Origin    { get; set; }
    public Vector3 Direction { get; set; }

    public Vector3 At(float t) => Origin + t * Direction;
}