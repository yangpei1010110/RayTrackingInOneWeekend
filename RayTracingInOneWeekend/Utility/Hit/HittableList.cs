namespace RayTracingInOneWeekend.Utility.Hit;

public class HittableList : IHittable
{
    private readonly List<IHittable> _objects = new();

    public bool Hit(Ray r, float tMin, float tMax, out HitRecord rec)
    {
        rec = default(HitRecord);
        bool hitAnything = false;
        float closestSoFar = tMax;

        foreach (IHittable obj in _objects)
        {
            if (obj.Hit(r, tMin, closestSoFar, out HitRecord tempRec))
            {
                hitAnything = true;
                closestSoFar = tempRec.T;
                rec = tempRec;
            }
        }

        return hitAnything;
    }

    public void Clear()            => _objects.Clear();
    public void Add(IHittable obj) => _objects.Add(obj);
}