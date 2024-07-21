using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Grid3D : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector2Int _cellSize = new(1, 1);
        [SerializeField] private Vector2Int _gridDimensions = new(10, 10);
        [SerializeField] private Vector2Int _offset;

        private readonly Dictionary<Cell, CellGameObject> _cellGameObjects = new();
        
        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            for (int x = 0; x < _gridDimensions.x; x++)
            {
                for (int y = 0; y < _gridDimensions.y; y++)
                {
                    Cell cell = new(x * (_cellSize.x + _offset.x), y * (_cellSize.y + _offset.y));
                    Vector2Int cellPosition = new(cell.X, cell.Y);
                    CellGameObject cellGameObject = CreateCell(cellPosition);
                    
                    _cellGameObjects.Add(cell, cellGameObject);
                }
            }
        }

        private CellGameObject CreateCell(Vector2Int position)
        {
            CellGameObject cellGameObject = new GameObject().AddComponent<CellGameObject>();
            cellGameObject.transform.position = new Vector3(position.x, 0f, position.y);
            cellGameObject.transform.localScale = new Vector3(_cellSize.x, 1f, _cellSize.y);
            cellGameObject.transform.parent = transform;
            cellGameObject.name = $"Cell_{position.x}_{position.y}";

            return cellGameObject;
        }
    }
}
