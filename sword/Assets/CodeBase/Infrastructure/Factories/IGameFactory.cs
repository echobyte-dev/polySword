using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(GameObject at);
  }
}