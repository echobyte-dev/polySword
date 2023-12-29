using CodeBase.Data;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
  public interface IStaticDataService
  {
    void Load();
    MonsterStaticData ForMonster(MonsterTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);
  }
}