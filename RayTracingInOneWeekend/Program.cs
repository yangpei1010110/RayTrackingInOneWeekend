// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;

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
            for (int i = 0; i < imageWidth; i++)
            {
                var r = (double)i / (imageWidth  - 1);
                var g = (double)j / (imageHeight - 1);
                var b = 0.25;

                var ir = (int)(255.999 * r);
                var ig = (int)(255.999 * g);
                var ib = (int)(255.999 * b);

                sb.AppendLine($"{ir} {ig} {ib}");
            }
        }

        File.WriteAllText(filePath, sb.ToString());
        // open the image
        Process.Start("explorer.exe", $"/select, {Path.GetFullPath(filePath)}");
    }
}