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

        public static int Random(int number) => RandomInstance.NextInt(number);
        
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
        
        /// <summary>
        /// Shuffles the elements in the list using the Durstenfeld implementation of the Fisher-Yates algorithm.
        /// This method modifies the input list in-place, ensuring each permutation is equally likely, and returns the list for method chaining.
        /// Reference: http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        /// <param name="list">The list to be shuffled.</param>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <returns>The shuffled list.</returns>
        public static IList<T> Shuffle<T>(this IList<T> list) 
        {
            var count = list.Count;
            while (count > 1) 
            {
                --count;
                var index = Random(count + 1);
                (list[index], list[count]) = (list[count], list[index]);
            }

            return list;
        }
    }
}