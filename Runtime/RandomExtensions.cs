using System.Collections.Generic;
using Unity.Mathematics;
using Yonii.Unity.Utilities.CustomTypes;

namespace Yonii.Unity.Utilities
{
    public static class RandomExtensions
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static Random RandomInstance = new((uint)UnityEngine.Random.Range(0, int.MaxValue) * 2 - 1);
        
        public static int NextIntInclusive(ref this Random random, IntRange range) => random.NextInt(range.Min, range.Max + 1);

        public static int NextIntRange(ref this Random random, IntRange range) => random.NextInt(range.Min, range.Max);
        public static int Random(ref this IntRange range) => RandomInstance.NextInt(range.Min, range.Max + 1);

        public static float Next(ref this Random random, FloatRange range) => random.NextFloat(range.Min, range.Max);
        public static int Random(int min, int max) => RandomInstance.NextInt(min, max + 1);
        public static int RandomWithoutInclusive(int min, int max) => RandomInstance.NextInt(min, max);
        public static float Random(ref this FloatRange range) => RandomInstance.NextFloat(range.Min, range.Max);

        public static float3 NextFloat3(ref this Random random, Vector3Range range) => random.NextFloat3(range.Min, range.Max);
        public static float3 Random(this Vector3Range range) => RandomInstance.NextFloat3(range.Min, range.Max);

        public static T Random<T>(this T[] array)
        {
            var index = Random(0, array.Length - 1);
            return array[index];
        }
        
        public static T Random<T>(this List<T> list)
        {
            var index = Random(0, list.Count - 1);
            return list[index];
        }
    }
}