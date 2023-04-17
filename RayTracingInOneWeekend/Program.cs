﻿using System.Diagnostics;
using System.Numerics;
using System.Text;
using RayTracingInOneWeekend;
using RayTracingInOneWeekend.Utility;

class Program
{
    static void Main(string[] args)
    {
        // Image

        const string  filePath    = @"image.ppm";
        const float   aspectRatio = 16.0f / 9.0f;
        const int     imageWidth  = 400;
        const int     imageHeight = (int)(imageWidth / aspectRatio);
        StringBuilder sb          = new StringBuilder();

        // Camera

        float viewportHeight = 2.0f;
        float viewportWidth  = aspectRatio * viewportHeight;
        float focalLength    = 1.0f;

        Vector3 origin          = Vector3.Zero;
        Vector3 horizontal      = new Vector3(viewportWidth, 0, 0);
        Vector3 vertical        = new Vector3(0, viewportHeight, 0);
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
                float u = i / (float)(imageWidth  - 1);
                float v = j / (float)(imageHeight - 1);

                Ray     r          = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
                Vector3 pixelColor = Tool.RayColor(r);
                Color.WriteColor(sb, pixelColor);
            }
        }

        File.WriteAllText(filePath, sb.ToString());
        // open the image
        Process.Start("explorer.exe", $"/select, {Path.GetFullPath(filePath)}");
    }
}