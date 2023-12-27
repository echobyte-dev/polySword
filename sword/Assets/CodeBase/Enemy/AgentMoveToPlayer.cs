using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    [SerializeField] private NavMeshAgent _agent;

    private Transform _playerTransform;

    public void Construct(Transform playerTransform)
    {
      _playerTransform = playerTransform;
    }

    private void Update() => 
      SetDestinationForAgent();

    private void SetDestinationForAgent()
    {
      if (_playerTransform)
        _agent.destination = _playerTransform.position;
    }
  }
}