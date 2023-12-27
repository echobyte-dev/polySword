using CodeBase.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Enemy
{
  public class RotateToPlayer : Follow
  {
    [SerializeField] private float _speed;
    
    private Transform _playerTransform;
    private Vector3 _positionToLook;
    
    public void Construct(Transform playerTransform)
    {
      _playerTransform = playerTransform;
    }
    
    private void Update()
    {
      if (IsInitialized())
        RotateTowardsPlayer();
    }
    
    private void RotateTowardsPlayer()
    {
      UpdatePositionToLookAt();

      transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDelta = _playerTransform.position - transform.position;
      _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
    }

    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
      Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private Quaternion TargetRotation(Vector3 position) =>
      Quaternion.LookRotation(position);

    private float SpeedFactor() =>
      _speed * Time.deltaTime;

    private bool IsInitialized() => 
      _playerTransform != null;
  }
}