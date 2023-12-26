using System;
using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(GameObject at);
    event Action PlayerCreated;
    GameObject PlayerGameObject { get; }
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();
  }
}