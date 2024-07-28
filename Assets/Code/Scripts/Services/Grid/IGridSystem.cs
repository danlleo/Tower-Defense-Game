using System.Collections.Generic;

namespace Services.Grid
{
    public interface IGridSystem : IService
    {
        uint Width { get; }
        uint Length { get; }

        TileData GetTileData(TilePosition tilePosition);
        (bool isPlaced, List<TilePosition> newTilePositions) TryPlaceObject(IGridObject objectToPlace, TilePosition at);
    }
}