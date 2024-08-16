using System.Collections.Generic;
using System.Linq;
using Services;
using Services.Grid;
using Services.Input;
using UnityEngine;

namespace Tower
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public class TowerPlacement : MonoBehaviour, IGridObject
    {
        [field: SerializeField] public Vector3 Size { get; private set; }
        public List<TilePosition> OccupiedTiles { get; private set; } = new();

        [SerializeField] private LayerMask _ignoreLayerMask;
        private IGridSystem _gridSystem;
        private IInputService _inputService;
        private Vector3 _mousePosition;
        private Vector3 _positionBeforeGrab;

        private void Awake()
        {
            _gridSystem = AllServices.Container.Single<IGridSystem>() ?? new GridSystem(new GridSystemSettings(40, 40, 1f));
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void OnMouseDown()
        {
            _positionBeforeGrab = transform.position;
        }

        private void OnMouseDrag()
        {
            _mousePosition = GetMouseWorldPosition();
            
            transform.position = _mousePosition;
        }

        private void OnMouseUp()
        {
            if (TryPlaceOnGrid(_mousePosition) is false)
                transform.position = _positionBeforeGrab;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 screenPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            return Physics.Raycast(ray, out RaycastHit hitInfo, 100f, _ignoreLayerMask)
                ? hitInfo.point : Vector3.zero;
        }

        public bool TryPlaceOnGrid(TilePosition tilePosition)
        {
            (bool isPlaced, List<TilePosition> newTilePositions) = _gridSystem.TryPlaceObject(this, at: tilePosition);
            
            if (!isPlaced)
                return false;

            OccupiedTiles = newTilePositions;
            transform.position = newTilePositions.First();
            return true;
        }
    }
}