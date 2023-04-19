namespace RayTracingInOneWeekend.Utility.Hit;

public interface IHittable
{
    bool Hit(Ray r, float tMin, float tMax, out HitRecord rec);
}