using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3._1.GalagaGame;

namespace Galaga_Exercise_3._1.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
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
            
        }
    }
}