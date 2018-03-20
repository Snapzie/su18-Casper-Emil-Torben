using DIKUArcade.Entities;
using Galaga_Exercise_3._1.GalagaEntities;

namespace Galaga_Exercise_3._1.MovementStrategy {
    public class Down : IMovementStrategy {
        private float s = 0.0003f;
        
        public void MoveEnemy(Enemy enemy) {
            enemy.Position.Y -= s;
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}