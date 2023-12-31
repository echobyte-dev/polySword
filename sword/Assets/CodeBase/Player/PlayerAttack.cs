﻿using CodeBase.Components;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Player
{
  [RequireComponent(typeof(PlayerAnimator), typeof(CharacterController))]
  public class PlayerAttack : MonoBehaviour, ISavedProgressReader
  {
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private CharacterController _characterController;

    private static int _layerMask;
    private Collider[] _hits = new Collider[3];
    private Stats _stats;
    private IInputService _inputService;

    private void Awake()
    {
      _inputService = GameBootstrapper.InputService;
      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }

    private void Update()
    {
      if (_inputService.IsAttackButtonUp() && !_playerAnimator.IsAttacking())
        _playerAnimator.PlayAttack();
    }

    public void OnAttack()
    {
      for (int i = 0; i < Hit(); i++)
      {
        _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
      }
    }

    public void LoadProgress(PlayerProgress progress) => 
      _stats = progress.PlayerStats;

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
  }
}