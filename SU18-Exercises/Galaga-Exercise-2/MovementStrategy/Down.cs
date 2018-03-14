using DIKUArcade.Entities;
using Galaga_Exercise_2.GalagaEntities;

namespace Galaga_Exercise_2.MovementStrategy {
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