using UnityEngine;
using Yonii.Unity.Utilities.Helpers;
// ReSharper disable InconsistentNaming

namespace Yonii.Unity.Utilities.MouseComponents
{
    public class FollowMouseComponent : MonoBehaviour
    {
        private Camera _camera;

        public LayerMask Mask;

        private void Awake() => _camera = Camera.main;

        private void Update()
        {
            var mousePosition = Mouse.WorldPosition(_camera, Mask, Mouse.GetCurrentMousePosition());
            transform.position = mousePosition;
        }
    }
}
