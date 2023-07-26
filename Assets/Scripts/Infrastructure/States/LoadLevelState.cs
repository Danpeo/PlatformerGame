using CameraLogic;
using Hud;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPoint);
            GameObject player = _gameFactory.CreatePlayer(initialPoint);
            
            _gameFactory.CreateHud();
            
            CameraFollow(player);
            
             _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(hero);
    }
}