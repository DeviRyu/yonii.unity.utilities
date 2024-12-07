using System.Collections.Generic;
using System.Linq;

namespace Yonii.Unity.Utilities
{
    public static class RandomUtils
    {
        private static readonly Dictionary<int, List<int>> _indexCountList = new();

        public static IReadOnlyList<int> GetRandomIndexes(int amount)
        {
            if (!_indexCountList.TryGetValue(amount, out var indexList)) 
                _indexCountList[amount] = indexList = Enumerable.Range(0, amount).ToList();

            indexList.Shuffle();
            return indexList;
        }
    }
}