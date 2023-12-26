using System;
using CodeBase.Components;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Player
{
  [RequireComponent(typeof(PlayerAnimator))]
  public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
  {
    [SerializeField] private PlayerAnimator _animator;
    private State _state;

    public Action HealthChange;

    public event Action HealthChanged;

    public float Current
    {
      get => _state.CurrentHP;
      set
      {
        if (_state.CurrentHP != value)
        {
          _state.CurrentHP = value;
          HealthChange?.Invoke();
        }
      }
    }

    public float Max
    {
      get => _state.MaxHP;
      set => _state.MaxHP = value;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      _state = progress.PlayerState;
      HealthChange?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.PlayerState.CurrentHP = Current;
      progress.PlayerState.MaxHP = Max;
    }

    public void TakeDamage(float damage)
    {
      if (Current <= 0)
        return;

      Current -= damage;
      _animator.PlayHit();
    }
  }
}