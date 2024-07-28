using System.Collections.Generic;
using UnityEngine;

namespace Services.Grid
{
    public interface IGridObject
    {
        Vector3 Size { get; }
        List<TilePosition> OccupiedTiles { get; }
    }
}