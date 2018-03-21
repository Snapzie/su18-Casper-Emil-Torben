using System;
using DIKUArcade.Entities;
using Galaga_Exercise_3._1.GalagaEntities;

namespace Galaga_Exercise_3._1.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        private float s = 0.0003f;
        private float p = 0.045f;
        private float a = 0.005f;

        public void MoveEnemy(Enemy enemy) {
            enemy.Position.Y -= s;
            enemy.Position.X = enemy.InitPosition.X + this.a * ((float) Math.Sin((2 * Math.PI * 
                 (0.9f - (enemy.Position.Y - s))) / p));
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}