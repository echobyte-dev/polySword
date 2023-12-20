﻿namespace CodeBase.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IPlayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}