using System.Diagnostics;
using System.Numerics;
using System.Text;
using RayTracingInOneWeekend.Utility;
using RayTracingInOneWeekend.Utility.Hit;

namespace RayTracingInOneWeekend;

internal static class Program
{
    private static void Main(string[] args)
    {
        // Image
        string filePath = Path.GetFullPath(@"image.ppm");
        float aspectRatio = 3.0f / 2.0f;
        int imageWidth = 1200;
        int imageHeight = (int)(imageWidth / aspectRatio);
        int samplesPerPixel = 256;
        int maxDepth = 50;
        StringBuilder sb = new();

        // World
        HittableList world = HittableList.RandomScene();

        // Camera
        Vector3 lookFrom = new(13, 2, 3);
        Vector3 lookAt = new(0, 0, 0);
        Vector3 vUp = new(0, 1, 0);
        float distToFocus = 10.0f;
        float aperture = 0.1f;
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
        Stopwatch sw = Stopwatch.StartNew();
        Parallel.For(0, imageHeight, j =>
        {
            for (int i = 0; i < imageWidth; i++)
            {
                // Vector3 pixelColor = Vector3.Zero;
                for (int s = 0; s < samplesPerPixel; s++)
                {
                    float u = (i + RandomTool.NextFloat()) / (imageWidth - 1);
                    float v = (j + RandomTool.NextFloat()) / (imageHeight - 1);

                    Ray r = camera.GetRay(u, v);
                    // pixelColor += Tool.RayColor(r, world);
                    pixels[j * imageWidth + i] += Tool.RayColor(r, world, maxDepth);
                }
            }

            lock (lockObj)
            {
                work++;
                Console.WriteLine($"\rScanLines remaining: {work / (float)imageHeight:P} {imageHeight - work} {sw.Elapsed}");
            }
        });
        sw.Stop();
        Console.WriteLine($"Finish in {sw.Elapsed}");

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