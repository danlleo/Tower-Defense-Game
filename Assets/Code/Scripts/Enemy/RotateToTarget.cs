using UnityEngine;

namespace Enemy
{
  public class RotateToTarget : MonoBehaviour
  {
    [SerializeField] private float _speed;

    private Transform _targetTransform;
    private Vector3 _positionToLook;

    public void Construct(Transform heroTransform)
      => _targetTransform = heroTransform;
    
    private void Update()
    {
      if (_targetTransform)
        RotateTowardsHero();
    }

    private void RotateTowardsHero()
    {
      UpdatePositionToLookAt();

      transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDelta = _targetTransform.position - transform.position;
      _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
    }
    
    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
      Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private Quaternion TargetRotation(Vector3 position) =>
      Quaternion.LookRotation(position);

    private float SpeedFactor() =>
      _speed * Time.deltaTime;
  }
}