using System.Numerics;

namespace RayTracingInOneWeekend.Utility;

public class Ray
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin    = origin;
        Direction = direction;
    }

    public Vector3 Origin    { get; set; }
    public Vector3 Direction { get; set; }

    public Vector3 At(float t)
    {
        return Origin + t * Direction;
    }
}