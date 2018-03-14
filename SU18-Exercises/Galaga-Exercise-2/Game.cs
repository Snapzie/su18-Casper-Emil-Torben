using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Physics;
using Galaga_Exercise_2.MovementStrategy;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private GameEventBus<object> eventBus;
        private Player player;
        private GameTimer gameTimer;
        private List<Image> enemyStrides;
        private ImageStride enemyAnimation;
        private EntityContainer enemies;
        private EntityContainer playerShots;
        private Image laser;
        private int numOfEnemies = 24;
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        private ISquadron eneFormation;
        private IMovementStrategy moveStrat;

        public Game() {
            // look at the Window.cs file for possible constructors.
            // We recommend using 500 × 500 as window dimensions,
            // which is most easily done using a predefined aspect ratio.
            win = new Window("Cool Game", 500, 500);
            gameTimer = new GameTimer(60, 60);
            
            player = new Player();
            
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
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            laser = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            
            // enemies = new EntityContainer(numOfEnemies);
            playerShots = new EntityContainer();
            
            explosions = new AnimationContainer(8);
           // enemyAnimation = new ImageStride(80, enemyStrides);
            
            // AddEnemies();

            eneFormation = new IsoscelesTriangleFormation(15);
            eneFormation.CreateEnemies(enemyStrides);
            
            moveStrat = new ZigZagDown();
            
            
        }
        
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        private void AddEnemies() {
            float height = 0.9f;
            for (int i = 0; i < numOfEnemies; i++) {
                enemies.AddDynamicEntity(new DynamicShape(new Vec2F((1.0f / 8) * (i % 8), height), 
                    new Vec2F(0.1f, 0.1f) ), enemyAnimation);  
                if ((i + 1) % 8 == 0 && i != 0) {
                    height -= 0.1f;
                }
            } 
        }

        private void Shoot() {
            DynamicShape shot = new DynamicShape(new Vec2F(player.Self.Shape.Position.X + 0.05f, 0.2f),
                new Vec2F(0.008f, 0.027f), new Vec2F(0, 0.01f));
            playerShots.AddDynamicEntity(shot, laser);
        }

        private void ShotIterator(Entity shot) {
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            }
        }
        
        //TODO: Somehow call this from ShotIterator would be nice
        private void EnemyIterator(Entity enemy) {}

        private void IterateShots() {
            foreach (Entity shot in playerShots) {
                foreach (Entity enemy in eneFormation.Enemies) {
                    if (CollisionDetection.Aabb((DynamicShape) shot.Shape, enemy.Shape).Collision) {
                        enemy.DeleteEntity();
                        shot.DeleteEntity();
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y, 
                            enemy.Shape.Extent.X, enemy.Shape.Extent.Y);
                    }
                }

                if (!shot.IsDeleted()) {
                    shot.Shape.Move();
                }
            }
            playerShots.Iterate(ShotIterator);
            eneFormation.Enemies.Iterate(EnemyIterator);
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
                    moveStrat.MoveEnemies(eneFormation.Enemies);
                    eneFormation.RenderFormation();
                    //enemies.RenderEntities();
                    explosions.RenderAnimations();
                    playerShots.RenderEntities();
                    player.Render();
                    IterateShots();
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
                eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this, "MOVE LEFT", "", ""));
                break;   
            case "KEY_RIGHT":
                eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this, "MOVE RIGHT", "", ""));
                break;
            case "KEY_SPACE":
                Shoot();
                break;
            }
        }

        public void KeyRelease(string key) {
            // match on e.g. "KEY_UP", "KEY_1", "KEY_A", etc.
            if (key == "KEY_LEFT" || key == "KEY_RIGHT") {
                eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this, "KEY RELEASE", "", ""));   
            }
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