using UnityEngine;
using UnityMouse = UnityEngine.InputSystem.Mouse;

namespace Yonii.Unity.Utilities.Helpers
{
    public static class Mouse
    {
        public static Vector3 WorldPosition(
            Camera camera,
            LayerMask mask,
            Vector2 currentPosition
            )
        {
            var mouseWorldPosition = Vector3.zero;
            var ray = camera.ScreenPointToRay(currentPosition);
            if (Physics.Raycast(ray, out var hit, 999f, mask))
                mouseWorldPosition = hit.point;
            return mouseWorldPosition;
        }

        /// <summary>
        /// Get Current Mouse Position via the new Input System
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetCurrentMousePosition()
            => UnityMouse.current.position.ReadValue();
    }
}
