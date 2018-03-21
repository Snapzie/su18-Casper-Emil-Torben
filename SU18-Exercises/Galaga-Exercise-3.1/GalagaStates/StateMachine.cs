using System;
using System.Collections;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3._1.GalagaGame;

namespace Galaga_Exercise_3._1.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
        private StateTransformer transformer = new StateTransformer();
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.MainMenu :
                    ActiveState = MainMenu.GetInstance();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            } else if (eventType == GameEventType.GameStateEvent) {
                switch (transformer.TransformStringToState(gameEvent.Parameter1)) {
                    case GameStateType.GameRunning:
                        ActiveState = GameRunning.GetInstance();
                        break;
                    case GameStateType.MainMenu:
                        ActiveState = MainMenu.GetInstance();
                        break;
                    case GameStateType.GamePaused:
                    ActiveState = GamePause.GetInstance();
                    break;
                    //TODO: Add pause state
                }

                
            }
        }
    }
}