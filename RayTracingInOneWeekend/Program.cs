using System.Diagnostics;
using System.Numerics;
using System.Text;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Hit;
using RayTracingInOneWeekend.Utility.Shape;

namespace RayTracingInOneWeekend;

internal class Program
{
    private static void Main(string[] args)
    {
        // Image
        string filePath = Path.GetFullPath(@"image.ppm");
        float aspectRatio = 16.0f / 9.0f;
        int imageWidth = 400;
        int imageHeight = (int)(imageWidth / aspectRatio);
        int samplesPerPixel = 100;
        StringBuilder sb = new();

        // World
        HittableList world = new();
        world.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
        world.Add(new Sphere(new Vector3(0, -100.5f, -1), 100));

        // Camera
        Camera camera = new();

        // Render

        sb.AppendLine("P3");
        sb.AppendLine($"{imageWidth} {imageHeight}");
        sb.AppendLine("255");

        for (int j = imageHeight - 1; j >= 0; j--)
        {
            Console.Error.WriteLine($"\rScanLines remaining: {j}");
            for (int i = 0; i < imageWidth; i++)
            {
                Vector3 pixelColor = Vector3.Zero;
                for (int s = 0; s < samplesPerPixel; s++)
                {
                    float u = (i + Random.Shared.NextSingle()) / (imageWidth - 1);
                    float v = (j + Random.Shared.NextSingle()) / (imageHeight - 1);

                    Ray r = camera.GetRay(u, v);
                    pixelColor += Tool.RayColor(r, world);
                }

                Color.WriteColor(sb, pixelColor, samplesPerPixel);
            }
        }

        File.WriteAllText(filePath, sb.ToString());
        // open the image
        Process.Start("explorer.exe", $"/select, {filePath}");
    }
}