using UnityEngine;

namespace Yonii.Unity.Utilities
{
    public static class PositionExtensions
    {
        /// <summary>
        /// Gets the height of the active terrain at the transform's current position.
        /// Returns the transform's current Y position if no terrain is active.
        /// </summary>
        /// <param name="transform">The transform whose position to sample.</param>
        /// <returns>The height of the terrain at the transform's position.</returns>
        public static float GetTerrainHeightAtPosition(this Transform transform)
        {
            var activeTerrain = Terrain.activeTerrain;

            if (!activeTerrain)
                return transform.position.y; // fallback

            var pos = transform.position;
            return activeTerrain.SampleHeight(pos) + activeTerrain.transform.position.y;
        }
        
        /// <summary>
        /// Returns a position projected onto the surface of the terrain below (or above) the specified position,
        /// with an optional Y offset. Falls back to casting upwards if nothing is hit below.
        /// </summary>
        /// <param name="position">The world position to project onto terrain.</param>
        /// <param name="offset">Optional offset to add to the resulting Y coordinate.</param>
        /// <returns>The adjusted position on the terrain surface, or Vector3.zero if no terrain was hit.</returns>
        public static Vector3 GetTerrainPosition(this Vector3 position, float offset = 0)
        {
            var terrainLayerMask = LayerMask.GetMask("Terrain");
            var terrainPosition = Vector3.zero;
            
            if(Physics.Raycast(position, Vector3.down, out var hit, maxDistance: 500f, terrainLayerMask))
                terrainPosition = hit.point;
            
            if(terrainPosition.Equals(Vector3.zero))
                if (Physics.Raycast(position, Vector3.up, out var reverseHit, 500f, layerMask: terrainLayerMask))
                    terrainPosition = reverseHit.point;

            terrainPosition.y += offset;
            return terrainPosition;
        } 
    }
}