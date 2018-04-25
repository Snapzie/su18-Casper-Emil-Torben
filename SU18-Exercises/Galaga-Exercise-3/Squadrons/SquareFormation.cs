using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.Squadrons {
    public class SquareFormation : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public SquareFormation(int maxEnemies) {
            if (maxEnemies > 32) {
                maxEnemies = 32;
            }
            this.MaxEnemies = maxEnemies;
            Enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            float height = 0.9f;
            for (int i = 0; i < this.MaxEnemies; i++) {
                Enemy enemy = new Enemy(new StationaryShape(new Vec2F((1.0f / 8) * (i % 8), height), 
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides));
                this.Enemies.AddDynamicEntity(enemy);
                if ((i + 1) % 8 == 0 && i != 0) {
                    height -= 0.1f;
                }
            }
        }

        public void RenderFormation() {
            Enemies.RenderEntities();
        }
    }
}