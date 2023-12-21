﻿namespace CodeBase.Data
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }

  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}