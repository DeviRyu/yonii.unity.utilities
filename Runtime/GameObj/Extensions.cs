using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// In very specific cases when you know that the position of a child will not change in a Prefab/GameObject then you can specify the indexes directly.
        /// Ex: indexes = [1,4,3] -> Get First Child -> Get Fourth Child of First Child -> Get Third Child of Fourth child.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="indexes"></param>
        /// <param name="childObject"></param>
        /// <param name="name">Optional parameter to perform a name check for the game object found.</param>
        /// <returns>Will return the child from the last specified index.</returns>
        public static bool TryGetChildUsingMultiLayerIndexing(
            this Transform transform,
            List<int> indexes,
            out GameObject childObject,
            string name = null
            )
        {
            foreach (var index in indexes)
            {
                Transform newTransform;
                try
                {
                    newTransform = transform.GetChild(index);
                }
                catch (Exception e)
                {
                    Debug.LogError(
                        $"Could not find child using index {index} in transform {transform.name}" +
                        $"Please check that index - {index} is still valid."
                        );
                    Debug.LogException(e);

                    childObject = null;
                    return false;
                }
                
                transform = newTransform;
            }

            if (!name.IsNullOrWhiteSpace() && transform.name != name)
            {
                Debug.LogWarning(
                    "Name check is false. " +
                    $"Expected name was {name} while the object name found was {transform.name}."
                    );
            }

            childObject = transform.gameObject;
            return true;
        }

        /// <summary>
        /// Will fully traverse each layer until it finds the child game object or until it hits max layer.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <param name="maxLayer"></param>
        /// <param name="childObject"></param>
        /// <returns></returns>
        public static bool TryGetChild(
            this Transform transform, 
            string name, 
            int maxLayer,
            out GameObject childObject
        )
        {
            var childCount = transform.childCount;
            var layer = 0;
            var layerGameObjectDic = new Dictionary<int, List<Transform>>();

            while (maxLayer != layer)
            {
                if (layer > maxLayer)
                {
                    Debug.LogWarning(
                        $"Max Layer value of {maxLayer} has been hit but we could not find {name} child object." +
                        $"Either increase the Max Layer value or check if the name {name} is correct." +
                        "If name is correct the check if your game object does actually contain that child." +
                        "Alternatively if you believe there is something wrong with the search please reach out!"
                        );

                    childObject = null;
                    return false;
                }
                
                List<Transform> previousLayerObjects = null;
                if (layer != 0)
                {
                    var previousLayer = layer - 1;
                    if (!layerGameObjectDic.TryGetValue(previousLayer, out previousLayerObjects))
                    {
                        Debug.LogWarning($"Could not find the previous layer (${previousLayer}) game objects.");
                        childObject = null;
                        return false;
                    }
                }

                var previousLayerObjectsCount = previousLayerObjects?.Count ?? 1;
                for (var l = 0; l < previousLayerObjectsCount; l++)
                {
                    if (previousLayerObjects != null)
                    {
                        transform = previousLayerObjects[l];
                        childCount = transform.childCount;
                    }

                    if (TrySearchForChildWhileBuildingLayerBasedDic(
                            childCount,
                            transform,
                            name,
                            layer,
                            layerGameObjectDic,
                            out childObject
                            )
                        )
                        return true;
                }
                
                layer++;
            }

            childObject = null;
            return false;
        }

        private static bool TrySearchForChildWhileBuildingLayerBasedDic(
            int childCount, 
            Transform transform,
            string name,
            int layer,
            Dictionary<int, List<Transform>> layerGameObjectDic, 
            out GameObject childObject
            )
        {
            for (var i = 0; i < childCount; i++)
            {
                var child = transform.GetChild(i);

                if (!child)
                {
                    Debug.LogWarning($"Could not find child at index {i}. " +
                                     "Make sure child objects are not being removed while searching.");
                    childObject = null;
                    return false;
                }
                
                if (child.name != name)
                {
                    if (!layerGameObjectDic.TryGetValue(layer, out var layerChildList))
                        layerGameObjectDic.TryAdd(layer, new List<Transform> { child });
                    else
                        layerChildList.Add(child);
                    
                    continue;
                }
                
                childObject = child.gameObject;
                return true;
            }

            childObject = null;
            return false;
        }
    }
}
