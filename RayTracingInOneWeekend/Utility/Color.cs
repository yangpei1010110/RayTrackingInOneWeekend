using System.Numerics;
using System.Text;

namespace RayTracingInOneWeekend.Utility;

public static class Color
{
    public static void WriteColor(StringBuilder sb, Vector3 pixelColor, int samplesPerPixel)
    {
        pixelColor *= 1.0f / samplesPerPixel;
        pixelColor = Vector3.SquareRoot(pixelColor); // gamma correction
        sb.Append((int)(256 * float.Clamp(pixelColor.X, 0f, 0.999f)));
        sb.Append(' ');
        sb.Append((int)(256 * float.Clamp(pixelColor.Y, 0f, 0.999f)));
        sb.Append(' ');
        sb.Append((int)(256 * float.Clamp(pixelColor.Z, 0f, 0.999f)));
        sb.AppendLine();
    }
}