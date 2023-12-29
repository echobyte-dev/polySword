using System;
using CodeBase.StaticData;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Data
{
  [Serializable]
  public class EnemySpawnerData
  {
    public string Id;
    public MonsterTypeId MonsterTypeId;
    public Vector3 Position;

    public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position)
    {
      Id = id;
      MonsterTypeId = monsterTypeId;
      Position = position;
    }
  }
}