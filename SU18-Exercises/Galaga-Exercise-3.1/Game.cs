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
                    stateMachine.ActiveState.GameLoop();
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                }
            }
        }
    }
}