using UnityEngine;
// ReSharper disable StaticMemberInGenericType

namespace Yonii.Unity.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new();
        private static bool _applicationIsQuitting;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning(
                        $"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again."
                    );
                    return null;
                }

                lock (_lock)
                {
                    if (_instance)
                    {
                        return _instance;
                    }

                    _instance = FindFirstObjectByType<T>();
                    if (FindObjectsByType<T>(sortMode: FindObjectsSortMode.None).Length > 1)
                    {
                        Debug.LogError(
                            "[Singleton] Something went really wrong - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it."
                        );

                        return _instance;
                    }

                    if (!_instance)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"(singleton) {typeof(T)}";

                        Debug.Log($"[Singleton] An instance of {typeof(T)} was created.");
                    }
                    else
                    {
                        Debug.Log($"[Singleton] Using instance already created: {_instance.gameObject.name}");
                    }

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _applicationIsQuitting = true;
            }
        }
    }
}
