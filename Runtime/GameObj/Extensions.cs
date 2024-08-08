using UnityEngine;

namespace Yonniie8.Unity.Utilities.GameObj
{
    public static class Extensions
    {
        /// <summary>
        /// Gets height of GameObject from either MeshRenderer/SkinnedMeshRenderer.
        /// Will also do a search in GameObject children if a renderer was not found on the GameObject itself.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool TryGetHeight(this GameObject gameObject, out float height)
        {
            Bounds? gameObjectBounds = null;

            try
            {
                if(gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer))
                    gameObjectBounds  = meshRenderer.bounds;

                if (gameObject.TryGetComponent<SkinnedMeshRenderer>(out var skinnedMeshRenderer))
                    gameObjectBounds = skinnedMeshRenderer.bounds;

                if (gameObjectBounds != null)
                {
                    height = gameObjectBounds.Value.size.y;
                    return true;
                }

                var childMeshRender = gameObject.GetComponentInChildren<MeshRenderer>();
                if (ReferenceEquals(childMeshRender, null))
                {
                    var childSkinnedMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                    var localExtents = childSkinnedMeshRenderer.localBounds.extents;

                    height = childSkinnedMeshRenderer.bounds.size.y - localExtents.y;
                    return true;
                }

                gameObjectBounds = childMeshRender.bounds;
                height = gameObjectBounds.Value.size.y;
                return true;
            }
            // TODO: WIP Add Logging
            catch (MissingComponentException)
            {
                try
                {
                }
                catch (MissingComponentException)
                {
                }
            }

            height = 0;
            return false;
        }
    }
}
