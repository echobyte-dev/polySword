using CodeBase.Infrastructure;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Player
{
  [RequireComponent(typeof(CharacterController))]
  public class PlayerMove : MonoBehaviour
  {
    [SerializeField] private float _movementSpeed;

    private CharacterController _characterController;
    private IInputService _inputService;
    
    private void Awake()
    {
      _inputService = GameBootstrapper.InputService;
      _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
      Vector3 movementVector = Vector3.zero;

      if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
      {
        movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;
      }

      movementVector += Physics.gravity;
      _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
    }
  }
}