using System;

namespace Yonii.Unity.Utilities.CustomTypes
{
    [Serializable]
    public struct FloatRange
    {
        public float Min;
    
        public float Max;

        public override string ToString()
        {
            return $"{{Min={Min} Max={Max}}}";
        }
    }
}