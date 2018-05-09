using System;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu,
        GameLost
    }

    public class StateTransformer {
        /// <summary>
        /// Translate a string to a state
        /// </summary>
        /// <param name="state">The string to be transformed</param>
        /// <returns>Returns the translated state</returns>
        /// <exception cref="ArgumentException"></exception>
        public GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GameRunning" :
                return GameStateType.GameRunning;
            case "GamePaused" :
                return GameStateType.GamePaused;
            case "MainMenu" :
                return GameStateType.MainMenu;
            case "GameLost" :
                return GameStateType.GameLost;
            default:
                throw new ArgumentException("Argument thrown in TransformStringToState");
            }
        }
        
        /// <summary>
        /// Translates a state to a string
        /// </summary>
        /// <param name="state">The state to be translated</param>
        /// <returns>Returns the translated string</returns>
        /// <exception cref="ArgumentException"></exception>
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