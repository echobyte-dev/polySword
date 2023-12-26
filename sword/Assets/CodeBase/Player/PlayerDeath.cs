﻿using UnityEngine;

namespace CodeBase.Player
{
  [RequireComponent(typeof(PlayerHealth))]
  public class PlayerDeath : MonoBehaviour
  {
    [SerializeField] private PlayerHealth _health;

    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerAttack _attack;
    [SerializeField] private PlayerAnimator _animator;

    [SerializeField] private GameObject _deathFx;
    private bool _isDead;

    private void Start()
    {
      _health.HealthChanged += HealthChanged;
    }

    private void OnDestroy()
    {
      _health.HealthChanged -= HealthChanged;
    }

    private void HealthChanged()
    {
      if (!_isDead && _health.Current <= 0) 
        Die();
    }

    private void Die()
    {
      _isDead = true;
      _move.enabled = false;
      _attack.enabled = false;
      _animator.PlayDeath();

      Instantiate(_deathFx, transform.position, Quaternion.identity);
    }
  }
}