namespace Yonii.Unity.Utilities
{
    public static class StringExtensions
    {
        private const uint PrimeNumber = 16777619;
        
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrEmpty(value);

        // ReSharper disable once InconsistentNaming
        public static int ComputeFNV1aHash(this string value)
        {
            var hash = 2166136261;
            foreach (var c in value) 
                hash = (hash ^ c) * PrimeNumber;

            return unchecked((int)hash);
        }
    }
}