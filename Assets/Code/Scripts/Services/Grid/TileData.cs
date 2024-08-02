namespace Services.Grid
{
    public class TileData
    {
        public TilePosition TilePosition { get; }
        public IGridObject GridObject { get; private set; }

        public TileData(TilePosition tilePosition) => 
            TilePosition = tilePosition;

        public bool HasGridObject() => 
            GridObject is not null;

        public void SetGridObject(IGridObject gridObject) => 
            GridObject = gridObject;
        
        public void RemoveGridObject() => 
            GridObject = null;
    }
}