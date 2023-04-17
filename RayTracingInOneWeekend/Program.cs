// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Numerics;
using System.Text;
using RayTracingInOneWeekend.Utility;

class Program
{
    const int    imageWidth  = 256;
    const int    imageHeight = 256;
    const string filePath    = @"image.ppm";

    static void Main(string[] args)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("P3");
        sb.AppendLine($"{imageWidth} {imageHeight}");
        sb.AppendLine("255");

        for (int j = imageHeight - 1; j >= 0; j--)
        {
            Console.Error.WriteLine($"\rScanLines remaining: {j}");
            for (int i = 0; i < imageWidth; i++)
            {
                Vector3 pixelColor = new Vector3(i / (float)(imageWidth - 1), j / (float)(imageHeight - 1), 0.25f);
                Color.WriteColor(sb, pixelColor);
            }
        }

        File.WriteAllText(filePath, sb.ToString());
        // open the image
        Process.Start("explorer.exe", $"/select, {Path.GetFullPath(filePath)}");
    }
}