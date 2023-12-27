using System;
using System.Collections.Generic;
using CodeBase.Components;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(GameObject at);
    GameObject CreateHud();
    event Action PlayerCreated;
    GameObject PlayerGameObject { get; }
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();
    void Register(ISavedProgressReader savedProgress);
  }
}