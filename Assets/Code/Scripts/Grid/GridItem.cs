using UnityEngine;

namespace Grid
{
    [DisallowMultipleComponent]
    public class GridItem : MonoBehaviour
    {
        public bool IsTaken { get; set; }
        public Cell Cell { get; set; }
        
        public void Place()
        {
            transform.position = new Vector3(Cell.X, 0f, Cell.Y);
            IsTaken = true;
        }

        public void Release()
        {
            IsTaken = false;
        }
    }
}
