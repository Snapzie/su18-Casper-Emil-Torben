using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;
using SpaceTaxi_1.SpaceTaxiGame;
using SpaceTaxi_1.SpaceTaxiStates;

namespace SpaceTaxi_1
{
    public class Game : IGameEventProcessor<object>
    {   public static GameTimer GameTimer;
        
        private Window win;
        private GameEventBus<object> eventBus;


        private StateMachine stateMachine;

        public Game()
        {
            // window
            win = new Window("Space Taxi Game v0.1", 700, AspectRatio.R1X1);

            // event bus
            eventBus = SpaceBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.GameStateEvent,
                GameEventType.InputEvent,      // key press / key release
                GameEventType.WindowEvent,     // messages to the window, e.g. CloseWindow()
                GameEventType.PlayerEvent,      // commands issued to the player object, e.g. move, destroy, receive health, etc.
                GameEventType.TimedEvent
            });
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);

            // game timer
            Game.GameTimer = new GameTimer(60, 60); // 60 UPS and FPS
            
            stateMachine = new StateMachine();
        }
        
        /// <summary>
        /// Main loop of the game. Runs untill the game has ended
        /// </summary>
        public void GameLoop()
        {
            while (win.IsRunning())
            {
                Game.GameTimer.MeasureTime();

                while (Game.GameTimer.ShouldUpdate())
                {
                    win.PollEvents();
                    eventBus.ProcessEvents();
                }

                if (Game.GameTimer.ShouldRender())
                {
                    win.Clear();
                    stateMachine.ActiveState.GameLoop();
                    win.SwapBuffers();
                }

                if (Game.GameTimer.ShouldReset())
                {
                    // 1 second has passed - display last captured ups and fps from the timer
                    win.Title = "Space Taxi | UPS: " + Game.GameTimer.CapturedUpdates + ", FPS: " +
                                Game.GameTimer.CapturedFrames;
                }
            }
        }

        /// <summary>
        /// Processes events
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="gameEvent">Game event</param>
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.WindowEvent)
            {
                switch (gameEvent.Message)
                {
                    case "CLOSE_WINDOW":
                        win.CloseWindow();
                        break;
                }
            }
        }
    }
}
