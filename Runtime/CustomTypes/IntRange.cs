using System;

namespace Yonii.Unity.Utilities.CustomTypes
{
    [Serializable]
    public struct IntRange
    {
        public int Min;
        public int Max;

        public override string ToString() => $"{{Min={Min} Max={Max}}}";
    }
}