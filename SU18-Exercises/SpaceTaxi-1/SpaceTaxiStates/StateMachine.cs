﻿using System;
using System.Collections;
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
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
            case GameStateType.GameRunning:
                if (ActiveState == MainMenu.GetInstance()) {
                    GameRunning.GetInstance().InitializeGameState();
                    ActiveState = GameRunning.GetInstance();
                    break;
                } else {
                    ActiveState = GameRunning.GetInstance();
                    break;   
                }
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GamePause.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            } else if (eventType == GameEventType.GameStateEvent) {
                SwitchState(transformer.TransformStringToState(gameEvent.Parameter1));
            }
        }
    }
}