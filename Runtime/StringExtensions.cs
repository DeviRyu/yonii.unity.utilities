namespace Yonii.Unity.Utilities
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrEmpty(value);
    }
}