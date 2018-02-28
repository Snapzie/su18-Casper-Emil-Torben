using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private GameEventBus<object> eventBus;
        private Entity player;

        public Game() {
            // look at the Window.cs file for possible constructors.
            // We recommend using 500 × 500 as window dimensions,
            // which is most easily done using a predefined aspect ratio.
            win = new Window("Cool Game", 500, 500);
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent // commands issued to the player object,
            });
            // e.g. move, destroy, receive health, etc.
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);

            player = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                eventBus.ProcessEvents();
                win.PollEvents();
                win.Clear();
                win.SwapBuffers();
                player.Shape.Move(); 
                player.RenderEntity();
            }
        }

        public void KeyPress(string key) {
            switch (key) {
                case "KEY_ESCAPE":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                    break;
                case "KEY_LEFT":
                    ((DynamicShape) (player.Shape)).Direction.X = -0.0001f;
                    break;
                case "KEY_RIGHT":
                    ((DynamicShape) (player.Shape)).Direction.X = 0.0001f;
                    break;
            }

            // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
            // TODO: use this method to start moving your player object
            //((DynamicShape) (player.Shape)).Direction.X = 0.0001f; // choose a fittingly small number
        }

        public void KeyRelease(string key) {
            // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
            ((DynamicShape) (player.Shape)).Direction.X = 0.0f;
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                default:
                    break;
                }
            } else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
                player.RenderEntity();
            }
        }
    }
}