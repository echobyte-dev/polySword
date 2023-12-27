using System.Collections.Generic;
using CodeBase.Components;
using CodeBase.Data;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(GameObject at);
    GameObject CreateHud();
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();
    void Register(ISavedProgressReader savedProgress);
    GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
  }
}