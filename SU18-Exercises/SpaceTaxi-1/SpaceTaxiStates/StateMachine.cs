using DIKUArcade.EventBus;
using DIKUArcade.State;
using SpaceTaxi_1.SpaceTaxiGame;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
        private StateTransformer transformer = new StateTransformer();
        
        public StateMachine() {
            SpaceBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            SpaceBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }
        private void SwitchState(GameEvent<object> gameEvent) {
            switch (transformer.TransformStringToState(gameEvent.Parameter1)) {
            case GameStateType.GameRunning:
                if (ActiveState == MainMenu.GetInstance()) {
                    SetLevel(int.Parse(gameEvent.Parameter2));
                    GameRunning.GetInstance().InitializeGameState();
                    ActiveState = GameRunning.GetInstance();
                    break;
                } else if (ActiveState == GameRunning.GetInstance()) {
                    SetLevel(int.Parse(gameEvent.Parameter2));
                    GameRunning.GetInstance().InitializeGameState();
                    break;   
                } else {
                    ActiveState = GameRunning.GetInstance();
                    break;
                }
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GameLost :
                ActiveState = GameLost.GetInstance();
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            } else if (eventType == GameEventType.GameStateEvent) {
                SwitchState(gameEvent);
            }
        }

        public void SetLevel(int levelNumber) {
            GameRunning.GetInstance().SetLevel(levelNumber);
        }
    }
}