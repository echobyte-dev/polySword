using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(GameObject at);
    GameObject CreateHud();
    GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    void CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId);
    LootPiece CreateLoot();

    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    
    void Cleanup();
  }
}