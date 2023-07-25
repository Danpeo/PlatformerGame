using Services;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine StateMachine { get; set; }
        
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}
