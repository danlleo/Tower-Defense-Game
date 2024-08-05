using UnityEngine;

namespace GridSystems
{
    [DisallowMultipleComponent]
    public class PlacementSystem : MonoBehaviour
    {
        [Header("External References")]
        [SerializeField] private GameObject _mouseIndicator;
        [SerializeField] private GameObject _cellIndicator;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Grid _grid;
        
        [SerializeField] private ObjectsDatabaseSO _database;
        [SerializeField] private GameObject _gridVisualization;
        
        private int _selectedObjectIndex = -1;

        private void Start()
        {
            StopPlacement();
        }

        public void StartPlacement(int id)
        {
            StopPlacement();
            _selectedObjectIndex = _database.ObjectData.FindIndex(data => data.ID == id);

            if (_selectedObjectIndex < 0)
            {
                Debug.LogError($"No ID found {id}");
                return;
            }
            
            _gridVisualization.SetActive(true);
            _cellIndicator.SetActive(true);
            _inputManager.OnClicked += PlaceStructure;
            _inputManager.OnExit += StopPlacement;
        }
        
        private void Update()
        {
            if (_selectedObjectIndex < 0)
                return;
            
            // TODO Refactor this using Input Service
            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            _mouseIndicator.transform.position = mousePosition;
            _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
        }

        private void PlaceStructure()
        {
            if (_inputManager.IsPointerOverUI())
            {
                return;
            }
            
            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            GameObject newObject = Instantiate(_database.ObjectData[_selectedObjectIndex].Prefab);
            newObject.transform.position = _grid.CellToWorld(gridPosition);
        }
        
        private void StopPlacement()
        {
            _selectedObjectIndex = -1;
            _gridVisualization.SetActive(false);
            _cellIndicator.SetActive(false);
            _inputManager.OnClicked -= PlaceStructure;
            _inputManager.OnExit -= StopPlacement;
        }
    }
}