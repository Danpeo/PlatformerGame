using System;
using System.Collections.Generic;
using Hud;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootStrapState)] = new BootStrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        
        public void Enter<TypeState>() where TypeState : class, IState
        {
            IState state = ChangeState<TypeState>();
            state.Enter();
        }

        public void Enter<TypeState, TPayLoad>(TPayLoad payLoad) where TypeState : class, IPayLoadState<TPayLoad>
        {
            TypeState state = ChangeState<TypeState>();
            state.Enter(payLoad);
        }

        private TypeState ChangeState<TypeState>() where TypeState : class, IExitableState
        {
            _activeState?.Exit();
            TypeState state = GetState<TypeState>();
            _activeState = state;
            
            return state;
        }

        private TypeState GetState<TypeState>() where TypeState : class, IExitableState => 
            _states[typeof(TypeState)] as TypeState;
    }
}