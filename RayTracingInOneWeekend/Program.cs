﻿using System.Diagnostics;
using System.Numerics;
using System.Text;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Hit;
using RayTracingInOneWeekend.Utility.Mat;
using RayTracingInOneWeekend.Utility.Shape;

namespace RayTracingInOneWeekend;

internal class Program
{
    private static void Main(string[] args)
    {
        // Image
        string filePath = Path.GetFullPath(@"image.ppm");
        float aspectRatio = 16.0f / 9.0f;
        int imageWidth = 800;
        int imageHeight = (int)(imageWidth / aspectRatio);
        int samplesPerPixel = 100;
        int maxDepth = 50;
        StringBuilder sb = new();

        // World
        HittableList world = new();

        IMaterial materialGround = new Lambertian(new Vector3(0.8f, 0.8f, 0.0f));
        IMaterial materialCenter = new Lambertian(new Vector3(0.1f, 0.2f, 0.5f));
        IMaterial materialLeft = new Dielectric(1.5f);
        IMaterial materialRight = new Metal(new Vector3(0.8f, 0.6f, 0.2f), 0.0f);

        world.Add(new Sphere(new Vector3(0, -100.5f, -1), 100, materialGround));
        world.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, materialCenter));
        world.Add(new Sphere(new Vector3(-1, 0, -1), 0.5f, materialLeft));
        world.Add(new Sphere(new Vector3(-1, 0, -1), -0.4f, materialLeft));
        world.Add(new Sphere(new Vector3(1, 0, -1), 0.5f, materialRight));

        // Camera
        Vector3 lookFrom = new(3, 3, 2);
        Vector3 lookAt = new(0, 0, -1);
        Vector3 vUp = new(0, 1, 0);
        float distToFocus = (lookFrom - lookAt).Length();
        float aperture = 2.0f;
        Camera camera = new(
            lookFrom,
            lookAt,
            vUp,
            30.0f,
            aspectRatio,
            aperture,
            distToFocus);

        // Render
        sb.AppendLine("P3");
        sb.AppendLine($"{imageWidth} {imageHeight}");
        sb.AppendLine("255");

        Vector3[] pixels = new Vector3[imageHeight * imageWidth];
        object lockObj = new();
        int work = 0;
        Parallel.For(0, imageHeight, j =>
        {
            for (int i = 0; i < imageWidth; i++)
            {
                // Vector3 pixelColor = Vector3.Zero;
                for (int s = 0; s < samplesPerPixel; s++)
                {
                    float u = (i + Random.Shared.NextSingle()) / (imageWidth - 1);
                    float v = (j + Random.Shared.NextSingle()) / (imageHeight - 1);

                    Ray r = camera.GetRay(u, v);
                    // pixelColor += Tool.RayColor(r, world);
                    pixels[j * imageWidth + i] += Tool.RayColor(r, world, maxDepth);
                }
            }

            lock (lockObj)
            {
                work++;
                Console.Error.WriteLine($"\rScanLines remaining: {work / (float)imageHeight:P} {imageHeight - work}");
            }
        });
        // for (int j = imageHeight - 1; j >= 0; j--)
        // {
        //     Console.Error.WriteLine($"\rScanLines remaining: {j}");
        //     for (int i = 0; i < imageWidth; i++)
        //     {
        //         for (int s = 0; s < samplesPerPixel; s++)
        //         {
        //             float u = (i + Random.Shared.NextSingle()) / (imageWidth - 1);
        //             float v = (j + Random.Shared.NextSingle()) / (imageHeight - 1);
        //
        //             Ray r = camera.GetRay(u, v);
        //             pixels[j * imageWidth + i] += Tool.RayColor(r, world, maxDepth);
        //         }
        //     }
        // }

        // write color to file
        for (int j = imageHeight - 1; j >= 0; j--)
        {
            for (int i = 0; i < imageWidth; i++)
            {
                Color.WriteColor(sb, pixels[j * imageWidth + i], samplesPerPixel);
            }
        }

        File.WriteAllText(filePath, sb.ToString());
        // open the image
        Process.Start("explorer.exe", $"/select, {filePath}");
    }
}