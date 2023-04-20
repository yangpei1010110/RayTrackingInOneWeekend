namespace RayTracingInOneWeekend.Utility.Extensions;

public static class RandomExtension
{
    public static float RandomFloat(this Random random, float min, float max) => min + random.NextSingle() * (max - min);
}