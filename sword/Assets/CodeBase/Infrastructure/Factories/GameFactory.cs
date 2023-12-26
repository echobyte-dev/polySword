using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;

    public GameObject PlayerGameObject { get; set; }
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public event Action PlayerCreated;

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }

    public GameObject CreatePlayer(GameObject at)
    {
      PlayerGameObject = InstantiateRegistered(AssetPath.PlayerPath, at.transform.position);
      PlayerCreated?.Invoke();
      return PlayerGameObject;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(prefabPath, at: at);
      
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if(progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);
      
      ProgressReaders.Add(progressReader);
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }
  }
}