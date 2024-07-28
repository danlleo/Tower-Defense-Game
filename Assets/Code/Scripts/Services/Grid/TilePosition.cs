using UnityEngine;

namespace Services.Grid
{
    public readonly struct TilePosition
    {
        public readonly uint X;
        public readonly uint Z;
        
        public Vector3 WorldCoordinates => this;

        public TilePosition(uint x, uint z) => 
            (X, Z) = (x, z);

        public static implicit operator Vector3(TilePosition tilePosition) =>
            new(tilePosition.X * GridSystem.TileScale, 
                0, 
                tilePosition.Z * GridSystem.TileScale);
        
        public static implicit operator TilePosition(Vector3 worldPosition) =>
            new((uint) Mathf.RoundToInt(worldPosition.x / GridSystem.TileScale), 
                (uint) Mathf.RoundToInt(worldPosition.z / GridSystem.TileScale));
    }
}