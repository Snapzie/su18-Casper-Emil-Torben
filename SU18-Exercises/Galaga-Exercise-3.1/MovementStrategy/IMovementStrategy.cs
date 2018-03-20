using DIKUArcade.Entities;
using Galaga_Exercise_3._1.GalagaEntities;

namespace Galaga_Exercise_3._1.MovementStrategy {
    public interface IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
}