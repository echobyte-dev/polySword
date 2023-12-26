using CodeBase.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    private const float MinimalDistance = 1;
    
    [SerializeField] private NavMeshAgent _agent;
    private Transform _playerTransform;
    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;

      if (_gameFactory.PlayerGameObject != null)
        InitializePlayerTransform();
      else
      {
        _gameFactory.PlayerCreated += PlayerCreated;
      }
    }

    private void Update()
    {
      if (Initialized() && PlayerNotReached())
        _agent.destination = _playerTransform.position;
    }

    private bool Initialized() => 
      _playerTransform != null;

    private void PlayerCreated() => 
      InitializePlayerTransform();

    private void InitializePlayerTransform() => 
      _playerTransform = _gameFactory.PlayerGameObject.transform;

    private bool PlayerNotReached() => 
      Vector3.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;
  }
}