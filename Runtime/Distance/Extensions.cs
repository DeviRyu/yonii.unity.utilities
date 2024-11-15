using UnityEngine;

namespace Yonii.Unity.Utilities.Distance
{
    public static class Extensions 
    {
        /// <summary>
        /// This method calculates how much distance is between fromPosition and toPosition
        /// 
        /// Use a squared float as it's more performant
        /// </summary>
        /// <param name="fromPosition">ex:</param>
        /// <param name="toPosition">transform.position of a component</param>
        /// <param name="distanceRangeSquared">distance range needs to be squared</param>
        /// <returns></returns>
        public static bool IsInRange(this Vector3 fromPosition, Vector3 toPosition, float distanceRangeSquared) => 
            fromPosition.DistanceToPosition(toPosition).sqrMagnitude <= distanceRangeSquared;

        /// <summary>
        /// Calculate distance from your MonoBehaviour to a Vector3
        /// </summary>
        /// <param name="fromMonoBehaviour">ex: a PlayerController</param>
        /// <param name="toPosition">transform.position of any component</param>
        /// <param name="distanceRangeSquared">This must be calculated</param>
        /// <typeparam name="T">Is constraint to MonoBehaviour</typeparam>
        /// <returns>Return a true if the position of the MonoBehaviour is in range</returns>
        public static bool IsInRange<T>(this T fromMonoBehaviour, Vector3 toPosition, float distanceRangeSquared) where  T : MonoBehaviour
        {
            var isInRange = fromMonoBehaviour.transform.position.IsInRange(toPosition, distanceRangeSquared);
            return isInRange;
        }

        public static bool IsInRange(this Transform transform,
                                     Transform toTransform,
                                     float distanceRangeSquared,
                                     float maxAngle = 360f
            )
        {
            var distanceToPosition = transform.position
                .DistanceToPosition(toTransform.position)
                .With(y: 0);
            
            return distanceToPosition.sqrMagnitude <= distanceRangeSquared && 
                   Vector3.Angle(transform.forward, distanceToPosition) < maxAngle;
        }

        public static float DistanceBetween(this MonoBehaviour firstObject, MonoBehaviour secondObject) => 
            DistanceBetween(firstObject.transform.position, secondObject.transform.position);

        public static float DistanceBetween(this Vector3 firstPosition, Vector3 secondPosition) => 
            Vector3.Distance(firstPosition, secondPosition);

        public static Vector3 DistanceToPosition(this Vector3 fromPosition, Vector3 toPosition) =>
            fromPosition - toPosition;
    }
}
