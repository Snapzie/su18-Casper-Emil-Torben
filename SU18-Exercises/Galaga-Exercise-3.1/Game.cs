using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using Galaga_Exercise_3._1.GalagaGame;
using Galaga_Exercise_3._1.GalagaStates;
using Galaga_Exercise_3._1.MovementStrategy;
using Galaga_Exercise_3._1.Squadrons;

namespace Galaga_Exercise_3._1 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private GameTimer gameTimer; 
        private GameEventBus<object> eventBus;
//        private Player player; //Flyttet til gameRunning
//        private List<Image> enemyStrides; //Flyttet sammen med alt nedenstående
//        private ImageStride enemyAnimation;
//        private EntityContainer enemies;
//        private EntityContainer playerShots;
//        private Image laser;
//        private int numOfEnemies = 24;
//        private List<Image> explosionStrides;
//        private AnimationContainer explosions;
//        private int explosionLength = 500;
//        private ISquadron eneFormation;
//        private IMovementStrategy moveStrat;
        private StateMachine stateMachine;
        
        public Game() {
            // look at the Window.cs file for possible constructors.
            // We recommend using 500 × 500 as window dimensions,
            // which is most easily done using a predefined aspect ratio.
            win = new Window("Cool Game", 500, 500);
            gameTimer = new GameTimer(60, 60);
            
            eventBus = GalagaBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.GameStateEvent,
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent // commands issued to the player object,
            });
            // e.g. move, destroy, receive health, etc.
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            
            stateMachine = new StateMachine();
            
        }
        
        
        
        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    eventBus.ProcessEvents();
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    stateMachine.ActiveState.RenderState();
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
            
        }
        
//        public void KeyPress(string key) {
//            switch (key) {
//            case "KEY_ESCAPE":
//                eventBus.RegisterEvent(
//                    GameEventFactory<object>.CreateGameEventForAllProcessors(
//                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
//                break;
//            case "KEY_LEFT":
//                eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
//                    GameEventType.PlayerEvent, this, "MOVE LEFT", "", ""));
//                break;   
//            case "KEY_RIGHT":
//                eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
//                    GameEventType.PlayerEvent, this, "MOVE RIGHT", "", ""));
//                break;
//            case "KEY_SPACE":
//                Shoot();
//                break;
//            }
//        }
//
//        public void KeyRelease(string key) {
//            // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
//            if (key == "KEY_LEFT" || key == "KEY_RIGHT") {
//                eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
//                    GameEventType.PlayerEvent, this, "KEY RELEASE", "", ""));   
//            }
//        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                default:
                    break;
                }
            }else if (eventType == GameEventType.InputEvent) {
                stateMachine.ActiveState.HandleKeyEvent(gameEvent.Parameter2, gameEvent.Parameter1);
            }
        }
    }
}