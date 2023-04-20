namespace RayTracingInOneWeekend.Utility.Extensions;

public static class RandomExtension
{
    public static float NextFloat(this Random random, float min, float max) => min + random.NextSingle() * (max - min);
}