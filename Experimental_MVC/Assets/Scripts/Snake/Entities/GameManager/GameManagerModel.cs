using Batuhan.MVC.Core;
using R3;
namespace SnakeExample.Entities.GameManager 
{
    public enum GameState
    {
        PressAny,
        Started,
        GameOver
    }
    public interface IGameManagerModel : IModel
    {
        void Initialize();
        void ChangeGameState(GameState state);

        ReadOnlyReactiveProperty<GameState> GameState { get; }
    }
    public class GameManagerModel : IGameManagerModel
    {
        private ReactiveProperty<GameState> _gameState;
        public ReadOnlyReactiveProperty<GameState> GameState => _gameState;
        public GameManagerModel()
        {
            _gameState = new ReactiveProperty<GameState>();
        }
        public void ChangeGameState(GameState state)
        {
            _gameState.Value = state;
        }
        public void Initialize()
        {
        }
        public void Dispose()
        {
        }
    }
}