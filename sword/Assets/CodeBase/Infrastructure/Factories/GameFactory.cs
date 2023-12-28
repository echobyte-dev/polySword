using System.Collections.Generic;
using CodeBase.Components;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;
    private IStaticDataService _staticData;
    private IRandomService _randomService;
    private IPersistentProgressService _progressService;

    private GameObject _playerGameObject { get; set; }
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService progressService)
    {
      _assets = assets;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
    }

    public GameObject CreatePlayer(GameObject at)
    {
      _playerGameObject = InstantiateRegistered(AssetPath.Player, at.transform.position);
      return _playerGameObject;
    }

    public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
    {
      _staticData.LoadMonsters();

      MonsterStaticData monsterData = _staticData.ForMonster(typeId);
      GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

      IHealth health = monster.GetComponent<IHealth>();
      health.Current = monsterData.Hp;
      health.Max = monsterData.Hp;

      monster.GetComponent<ActorUI>().Construct(health);
      monster.GetComponent<AgentMoveToPlayer>()?.Construct(_playerGameObject.transform);
      monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

      monster.GetComponent<RotateToPlayer>()?.Construct(_playerGameObject.transform);

      var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
      lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
      lootSpawner.Construct(this, _randomService);

      Attack attack = monster.GetComponent<Attack>();
      attack.Construct(_playerGameObject.transform);
      attack.Damage = monsterData.Damage;
      attack.Cleavage = monsterData.Cleavage;
      attack.EffectiveDistance = monsterData.EffectiveDistance;
      
      return monster;
    }

    public LootPiece CreateLoot()
    {
      LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot)
        .GetComponent<LootPiece>();
      
      lootPiece.Construct(_progressService.Progress.WorldData);
      
      return lootPiece;
    }

    public GameObject CreateHud()
    {
      GameObject hud = InstantiateRegistered(AssetPath.Hud);
      
      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.WorldData);
      
      return hud;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(prefabPath, at: at);

      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);

      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }
  }
}