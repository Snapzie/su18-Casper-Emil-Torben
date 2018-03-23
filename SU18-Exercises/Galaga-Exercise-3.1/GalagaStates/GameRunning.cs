using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga_Exercise_3._1.GalagaGame;
using Galaga_Exercise_3._1.MovementStrategy;
using Galaga_Exercise_3._1.Squadrons;

namespace Galaga_Exercise_3._1 {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
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

        private GameRunning() {
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public void GameLoop() {
            this.IterateShots();
            this.RenderState();
        }
        
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        private void AddEnemies() {
            eneFormation = new SquareFormation(32);
            eneFormation.CreateEnemies(enemyStrides);
        }

        private void Shoot() {
            DynamicShape shot = new DynamicShape(new Vec2F(player.PlayerEntity.Shape.Position.X + 0.05f, 0.2f),
                new Vec2F(0.008f, 0.027f), new Vec2F(0, 0.01f));
            playerShots.AddDynamicEntity(shot, laser);
        }

        private void ShotIterator(Entity shot) {
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            }
        }
        
        private void EnemyIterator(Entity enemy) {
            if (enemy.Shape.Position.Y < 0.1f) {
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
            }
            
            //Collision is not detected
            if (CollisionDetection.Aabb( (DynamicShape) enemy.Shape, 
                (Shape) player.PlayerEntity.Shape).Collision) {
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
            }
        }

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
            
            if (eneFormation.Enemies.CountEntities() == 0) {
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameWon", ""));
            }
        }

        public void InitializeGameState() {
            player = new Player();
            
            enemyStrides =
                ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(8);
            
            laser = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            playerShots = new EntityContainer();
            
            moveStrat = new ZigZagDown();
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
            
            //Instantiates exercise 2 week 6
            AddEnemies();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            player.Move();
            moveStrat.MoveEnemies(eneFormation.Enemies);
            eneFormation.RenderFormation();
            explosions.RenderAnimations();
            playerShots.RenderEntities();
            player.Render();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                case "KEY_SPACE":
                    Shoot();
                    break;
                case "KEY_LEFT":
                    GalagaGame.GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE LEFT", "", ""));
                    break;   
                case "KEY_RIGHT":
                    GalagaGame.GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE RIGHT", "", ""));
                    break;
                case "KEY_ESCAPE":
                    GalagaGame.GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GamePaused", ""));
                    break;
                }
            } else {
                switch (keyValue) {
                case "KEY_LEFT":
                    player.KeyRelease();
                    break;
                case "KEY_RIGHT":
                    player.KeyRelease();
                    break;
                }
            }
        }
    }
}