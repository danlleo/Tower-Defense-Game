using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GridSystem
{
    [DisallowMultipleComponent]
    public class InputManager : MonoBehaviour
    {
        [Header("External References")]
        [SerializeField] private Camera _sceneCamera;

        [Header("Settings")] 
        [SerializeField] private LayerMask _placementLayerMask;
        [SerializeField] private float _detectionDistance = 100f;

        public event Action OnClicked;
        public event Action OnExit;
        
        private Vector3 _lastPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClicked?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnExit?.Invoke();
            }
        }

        // TODO: CLEAN THIS LATER
        public bool IsPointerOverUI()
            => EventSystem.current.IsPointerOverGameObject();
        
        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            // We use nearClipPlanes, so we wouldn't be able to select objects that aren't rendered by the camera
            mousePosition.z = _sceneCamera.nearClipPlane; 
            Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, _detectionDistance, _placementLayerMask))
            {
                _lastPosition = hit.point;
            }

            return _lastPosition;
        }
    }
}
