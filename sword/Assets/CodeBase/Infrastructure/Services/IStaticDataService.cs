using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
  public interface IStaticDataService
  {
    void LoadMonsters();
    MonsterStaticData ForMonster(MonsterTypeId typeId);
  }
}