using UnityEngine;

namespace Grid
{
    public class Snapper : MonoBehaviour
    {
        private Camera _camera;
        private GridItem _heldGridItem;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PerformRaycast();
            }
            
            TryDragObject();
        }

        private void PerformRaycast()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            if (hit.collider.TryGetComponent(out GridItem gridItem))
            {
                _heldGridItem = gridItem;
                _heldGridItem.IsTaken = true;
                return;
            }

            _heldGridItem = null;
        }

        private void TryDragObject()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            float planeDistance = _camera.nearClipPlane;
            
            if (_heldGridItem != null)
            {
                _heldGridItem.transform.position = ray.GetPoint(planeDistance) + Vector3.one;
            }
        }
    }
}
