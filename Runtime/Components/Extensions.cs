using UnityEngine;

namespace Yonii8.Unity.Utilities.Components
{
    public static class Extensions
    {
        /// <summary>
        /// TryGet variant of GetComponentInChildren
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(
            this Transform transform,
            out T component
            )
        {
            component = transform.GetComponentInChildren<T>();
            if (component == null)
            {
                Debug.LogWarning(
                    $"Could not find component of type {nameof(T)} in gameObject {transform.gameObject.name}");
            }
            
            return component != null;
        }

        /// <summary>
        /// TryGet variant of GetComponentsInChildren
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="components"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetComponentsInChildren<T>(this Transform transform, out T[] components)
        {
            components = transform.GetComponentsInChildren<T>();
            if (components == null || components.Length == 0)
            {
                Debug.LogWarning(
                    $"Could not find components of type {typeof(T)} in gameObject {transform.gameObject.name}");
            }

            return components is { Length: > 0 };
        }

        public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] components) =>
            gameObject.transform.TryGetComponentsInChildren(out components);
    }
}