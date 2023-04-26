using System.Runtime.CompilerServices;
using MathNet.Numerics.Random;

namespace RayTracingInOneWeekend;

public static class RandomTool
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat() => MersenneTwister.Default.NextSingle();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat(float min, float max) => min + MersenneTwister.Default.NextSingle() * (max - min);
}