using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Galaga_Exercise_1 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private GameEventBus<object> eventBus;
        private Player player;
        private GameTimer gameTimer;
        private List<Image> enemyStrides;
        private ImageStride enemyAnimation;
        private EntityContainer enemies;
        private int numOfEnemies = 24;

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
            
            player = new Player();
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            gameTimer = new GameTimer(60, 60);

            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemyAnimation = new ImageStride(80, enemyStrides);
            enemies = new EntityContainer(numOfEnemies);
            AddEnemies();
        }

        private void AddEnemies() {
            float height = 0.9f;
            int index = 0;
            for (int i = 0; i < numOfEnemies; i++) {
                enemies.AddDynamicEntity(new DynamicShape(new Vec2F((1.0f/8) * index, height), 
                    new Vec2F(0.1f, 0.1f) ), enemyAnimation);  
                
                index++;
                if (index % 8 == 0) {
                    height -= 0.1f;
                    index = 0;
                }
            } 
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    eventBus.ProcessEvents();
                }

                if (gameTimer.ShouldRender()) {
                    player.Move();
                    win.Clear();
                    enemies.RenderEntities();
                    player.Render();
                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
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
                player.MoveLeft();
                break;   
            case "KEY_RIGHT":
                player.MoveRight();  
                break;
            }
        }

        public void KeyRelease(string key) {
            // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
            player.KeyRelease();
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
            }else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
                player.Render();
            }
        }
    }
}