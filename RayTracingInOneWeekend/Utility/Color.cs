using System.Numerics;
using System.Text;

namespace RayTracingInOneWeekend.Utility;

public static class Color
{
    public static void WriteColor(StringBuilder sb, Vector3 pixelColor, int samplesPerPixel)
    {
        float scale = 1.0f / samplesPerPixel;
        pixelColor *= scale;
        pixelColor = new Vector3(MathF.Sqrt(pixelColor.X), MathF.Sqrt(pixelColor.Y), MathF.Sqrt(pixelColor.Z));
        sb.Append((int)(256 * float.Clamp(pixelColor.X, 0f, 0.999f)));
        sb.Append(' ');
        sb.Append((int)(256 * float.Clamp(pixelColor.Y, 0f, 0.999f)));
        sb.Append(' ');
        sb.Append((int)(256 * float.Clamp(pixelColor.Z, 0f, 0.999f)));
        sb.AppendLine();
    }
}