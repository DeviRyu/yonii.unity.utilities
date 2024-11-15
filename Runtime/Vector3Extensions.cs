﻿using UnityEngine;

namespace Yonii.Unity.Utilities
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
            new(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
}