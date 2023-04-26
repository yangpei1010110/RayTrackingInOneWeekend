using System.Numerics;
using RayTracingInOneWeekend.Utility.Mat;
using RayTracingInOneWeekend.Utility.Shape;

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

    public static HittableList RandomScene()
    {
        HittableList world = new();

        var groundMaterial = new Lambertian(new Vector3(0.5f, 0.5f, 0.5f));
        world.Add(new Sphere(new Vector3(0, -1000, 0), 1000, groundMaterial));

        for (int a = -11; a < 11; a++)
        {
            for (int b = -11; b < 11; b++)
            {
                float chooseMat = RandomTool.NextFloat();
                Vector3 center = new(a + 0.9f * RandomTool.NextFloat(), 0.2f, b + 0.9f * RandomTool.NextFloat());

                if ((center - new Vector3(4, 0.2f, 0)).Length() > 0.9f)
                {
                    if (chooseMat < 0.8f)
                    {
                        // diffuse
                        Vector3 albedo = Tool.RandomVector3() * Tool.RandomVector3();
                        var sphereMaterial = new Lambertian(albedo);
                        world.Add(new Sphere(center, 0.2f, sphereMaterial));
                    }
                    else if (chooseMat < 0.95f)
                    {
                        // metal
                        Vector3 albedo = Tool.RandomVector3(0.5f, 1f);
                        float fuzz = RandomTool.NextFloat(0, 0.5f);
                        var sphereMaterial = new Metal(albedo, fuzz);
                        world.Add(new Sphere(center, 0.2f, sphereMaterial));
                    }
                    else
                    {
                        // glass
                        var sphereMaterial = new Dielectric(1.5f);
                        world.Add(new Sphere(center, 0.2f, sphereMaterial));
                    }
                }
            }
        }

        var material1 = new Dielectric(1.5f);
        world.Add(new Sphere(new Vector3(0, 1, 0), 1.0f, material1));

        var material2 = new Lambertian(new Vector3(0.4f, 0.2f, 0.1f));
        world.Add(new Sphere(new Vector3(-4, 1, 0), 1.0f, material2));

        var material3 = new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0.0f);
        world.Add(new Sphere(new Vector3(4, 1, 0), 1.0f, material3));

        return world;
    }
}