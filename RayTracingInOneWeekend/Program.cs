using System.Diagnostics;
using System.Numerics;
using System.Text;
using RayTracingInOneWeekend;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Hit;
using RayTracingInOneWeekend.Utility.Shape;

internal class Program
{
    private static void Main(string[] args)
    {
        // Image

        const string filePath = @"image.ppm";
        const float aspectRatio = 16.0f / 9.0f;
        const int imageWidth = 400;
        const int imageHeight = (int)(imageWidth / aspectRatio);
        StringBuilder sb = new();

        // World
        HittableList world = new();
        world.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
        world.Add(new Sphere(new Vector3(0, -100.5f, -1), 100));

        // Camera

        float viewportHeight = 2.0f;
        float viewportWidth = aspectRatio * viewportHeight;
        float focalLength = 1.0f;

        Vector3 origin = Vector3.Zero;
        Vector3 horizontal = new(viewportWidth, 0, 0);
        Vector3 vertical = new(0, viewportHeight, 0);
        Vector3 lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focalLength);

        // Render

        sb.AppendLine("P3");
        sb.AppendLine($"{imageWidth} {imageHeight}");
        sb.AppendLine("255");

        for (int j = imageHeight - 1; j >= 0; j--)
        {
            Console.Error.WriteLine($"\rScanLines remaining: {j}");
            for (int i = 0; i < imageWidth; i++)
            {
                float u = i / (float)(imageWidth - 1);
                float v = j / (float)(imageHeight - 1);

                Ray r = new(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
                Vector3 pixelColor = Tool.RayColor(r, world);
                Color.WriteColor(sb, pixelColor);
            }
        }

        File.WriteAllText(filePath, sb.ToString());
        // open the image
        Process.Start("explorer.exe", $"/select, {Path.GetFullPath(filePath)}");
    }
}