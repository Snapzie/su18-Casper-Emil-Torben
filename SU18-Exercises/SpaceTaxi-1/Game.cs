using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace SpaceTaxi_1
{
    public class Game : IGameEventProcessor<object>
    {
        private Window _win;
        private GameEventBus<object> _eventBus;
        private GameTimer _gameTimer;

        private Entity _backGroundImage;
        private Player _player;

        public Game()
        {
            // window
            _win = new Window("Space Taxi Game v0.1", 500, AspectRatio.R1X1);

            // event bus
            _eventBus = new GameEventBus<object>();
            _eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent,      // key press / key release
                GameEventType.WindowEvent,     // messages to the window, e.g. CloseWindow()
                GameEventType.PlayerEvent      // commands issued to the player object, e.g. move, destroy, receive health, etc.
            });
            _win.RegisterEventBus(_eventBus);

            // game timer
            _gameTimer = new GameTimer(60); // 60 UPS, no FPS limit

            // game assets
            _backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );
            _backGroundImage.RenderEntity();

            // game entities
            _player = new Player();
            _player.SetPosition(0.45f, 0.6f);
            _player.SetExtent(0.1f, 0.1f);

            // event delegation
            _eventBus.Subscribe(GameEventType.InputEvent, this);
            _eventBus.Subscribe(GameEventType.WindowEvent, this);
            _eventBus.Subscribe(GameEventType.PlayerEvent, _player);
        }

        public void GameLoop()
        {
            while (_win.IsRunning())
            {
                _gameTimer.MeasureTime();

                while (_gameTimer.ShouldUpdate())
                {
                    _win.PollEvents();
                    _eventBus.ProcessEvents();
                }

                if (_gameTimer.ShouldRender())
                {
                    _win.Clear();
                    _backGroundImage.RenderEntity();
                    _player.RenderPlayer();
                   
                    _win.SwapBuffers();
                }

                if (_gameTimer.ShouldReset())
                {
                    // 1 second has passed - display last captured ups and fps from the timer
                    _win.Title = "Space Taxi | UPS: " + _gameTimer.CapturedUpdates + ", FPS: " +
                                _gameTimer.CapturedFrames;
                }
            }
        }

        public void KeyPress(string key)
        {
            switch (key)
            {
                case "KEY_ESCAPE":
                    _win.CloseWindow();
                    break;
                case "KEY_F12":
                    Console.WriteLine("Saving screenshot");
                    _win.SaveScreenShot();
                    break;
                case "KEY_UP":
                    _eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "BOOSTER_UPWARDS", "", ""));
                    break;
                case "KEY_LEFT":
                    _eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "BOOSTER_TO_LEFT", "", ""));
                    break;
                case "KEY_RIGHT":
                    _eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "BOOSTER_TO_RIGHT", "", ""));
                    break;
            }
        }

        public void KeyRelease(string key)
        {
            switch (key)
            {
                case "KEY_LEFT":
                    _eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "STOP_ACCELERATE_LEFT", "", ""));
                    break;
                case "KEY_RIGHT":
                    _eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "STOP_ACCELERATE_RIGHT", "", ""));
                    break;
                case "KEY_UP":
                    _eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "STOP_ACCELERATE_UP", "", ""));
                    break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.WindowEvent)
            {
                switch (gameEvent.Message)
                {
                    case "CLOSE_WINDOW":
                        _win.CloseWindow();
                        break;
                    default:
                        break;
                }
            }
            else if (eventType == GameEventType.InputEvent)
            {
                switch (gameEvent.Parameter1)
                {
                    case "KEY_PRESS":
                        KeyPress(gameEvent.Message);
                        break;
                    case "KEY_RELEASE":
                        KeyRelease(gameEvent.Message);
                        break;
                }
            }
        }
    }
}
