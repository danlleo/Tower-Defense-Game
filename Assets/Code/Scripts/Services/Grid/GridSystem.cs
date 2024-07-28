using System.Collections.Generic;

namespace Services.Grid
{
    public class GridSystem : IGridSystem
    {
        public static float TileScale { get; private set; }
        
        public uint Width { get; }
        public uint Length { get; }

        private TileData[,] Tiles { get; }
        
        public GridSystem(GridSystemSettings gridSystemSettings)
        {
            Width = gridSystemSettings.width;
            Length = gridSystemSettings.length;
            TileScale = gridSystemSettings.cellScale;
            Tiles = new TileData[Width, Length];

            for (uint x = 0; x < Width; x++)
            for (uint z = 0; z < Length; z++)
                Tiles[x, z] = new TileData(new TilePosition(x, z));
        }

        public TileData GetTileData(uint x, uint z) =>
            GetTileData(new TilePosition(x, z));

        public TileData GetTileData(TilePosition tilePosition) =>
            IsValidTilePosition(tilePosition) ? Tiles[tilePosition.X, tilePosition.Z] : null;

        public (bool isPlaced, List<TilePosition> newTilePositions) TryPlaceObject(IGridObject objectToPlace, TilePosition at)
        {
            (bool canPlace, List<TilePosition> checkedPositions) = CanPlaceObject(objectToPlace, at);

            if (!canPlace)
                return (false, null);
            
            foreach (TilePosition oldPosition in objectToPlace.OccupiedTiles) 
                    GetTileData(oldPosition).SetGridObject(null);

            foreach (TilePosition newPosition in checkedPositions) 
                    GetTileData(newPosition).SetGridObject(objectToPlace);

            return (true, checkedPositions);
        }

        private (bool canPlace, List<TilePosition> checkedPositions) CanPlaceObject(IGridObject objectToPlace, TilePosition at)
        {
            List<TilePosition> checkedPositions = new();
            
            for (uint x = 0; x < objectToPlace.Size.x; x++)
            for (uint z = 0; z < objectToPlace.Size.z; z++)
            {
                TilePosition tilePosition = new(at.X + x, at.Z + z);
                TileData tile = GetTileData(tilePosition);

                if (!IsValidTilePosition(tilePosition))
                    return (false, null);
                
                if (tile.HasGridObject() && tile.GridObject != objectToPlace)
                    return (false, null);

                checkedPositions.Add(tilePosition);
            }

            return (true, checkedPositions);
        }

        private bool IsValidTilePosition(uint x, uint z) =>
            IsValidTilePosition(new TilePosition(x, z));
        
        private bool IsValidTilePosition(TilePosition tilePosition) =>
            tilePosition.X < Width &&
            tilePosition.Z < Length;
    }
}