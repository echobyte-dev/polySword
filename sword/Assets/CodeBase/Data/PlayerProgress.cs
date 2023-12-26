﻿using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public State PlayerState;
    public WorldData WorldData;
    public Stats PlayerStats;

    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      PlayerState = new State();
      PlayerStats = new Stats();
    }
  }
}