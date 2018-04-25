﻿using System;

namespace Galaga_Exercise_3.GalagaStates {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu
    }

    public class StateTransformer {
        public GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GameRunning" :
                    return GameStateType.GameRunning;
                case "GamePaused" :
                    return GameStateType.GamePaused;
                case "MainMenu" :
                    return GameStateType.MainMenu;
                default:
                    throw new ArgumentException("Argument thrown in TransformStringToState");
            }
        }

        public string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning :
                    return "GameRunning";
                case GameStateType.GamePaused :
                    return "GamePaused";
                case GameStateType.MainMenu :
                    return "MainMenu";
                default:
                    throw new ArgumentException("Argument thrown in TransformStateToString");
            }
        }
    }
}