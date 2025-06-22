using UnityEngine;

namespace Yonii.Unity.Utilities
{
    public static class TransformExtensions
    {
        public static Vector3 Forward(this Transform transform, float distance)
        {
            return transform.position + transform.forward * distance;
        }
    }
}