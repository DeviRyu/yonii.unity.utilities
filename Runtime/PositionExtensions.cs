using UnityEngine;

namespace Yonii.Unity.Utilities
{
    public static class PositionExtensions
    {
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