using System.Numerics;
using System.Text;

namespace RayTracingInOneWeekend.Utility;

public static class Color
{
    public static void WriteColor(StringBuilder sb, Vector3 pixelColor, int samplesPerPixel)
    {
        float scale = 1.0f / samplesPerPixel;
        // pixelColor *= scale;
        sb.Append((int)(255.999f * float.Clamp(pixelColor.X * scale, 0f, 0.999f)));
        sb.Append(' ');
        sb.Append((int)(255.999f * float.Clamp(pixelColor.Y * scale, 0f, 0.999f)));
        sb.Append(' ');
        sb.Append((int)(255.999f * float.Clamp(pixelColor.Z * scale, 0f, 0.999f)));
        sb.AppendLine();
        // sb.AppendLine($"{(int)(255.999f * pixelColor.X)} {(int)(255.999f * pixelColor.Y)} {(int)(255.999f * pixelColor.Z)}");
    }
}