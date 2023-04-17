using System.Numerics;
using System.Text;

namespace RayTracingInOneWeekend.Utility;

public static class Color
{
    public static void WriteColor(StringBuilder sb, Vector3 pixelColor)
    {
        sb.AppendLine($"{(int)(255.999f * pixelColor.X)} {(int)(255.999f * pixelColor.Y)} {(int)(255.999f * pixelColor.Z)}");
    }
}