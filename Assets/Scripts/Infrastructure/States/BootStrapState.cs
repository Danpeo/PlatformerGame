using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootStrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>("Main");

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());

            _services.RegisterSingle<IAssets>(new AssetProvider());

            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssets>()));
        }

        public void Exit()
        {
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}