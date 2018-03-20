using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.Squadrons {
    public class IsoscelesTriangleFormation : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public IsoscelesTriangleFormation(int maxEnemies) {
            if (maxEnemies > 15) {
                maxEnemies = 15;
            }
            this.MaxEnemies = maxEnemies;
            Enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            //Finder højeste antal lag i en trekant der kan laves med MaxEnemies elementer
            int layers = (int) Math.Floor(0.5 * (Math.Sqrt(8 * MaxEnemies + 1) - 1));
            int currentLayer = 1;
            //Mellemrum mellem hver enemy
            const float space = 0.1f; 
            for (int i = 0; i < layers; i++) {
                for (int j = 0; j < currentLayer; j++) {
                    float x = 0.55f - (currentLayer * (0.1f)) + (0.1f + space) * j;
                    float y = 0.9f - ((currentLayer - 1) * 0.1f);
                    Enemy enemy = new Enemy(new StationaryShape(x, y, 0.1f, 0.1f), new ImageStride(80, enemyStrides));
                    Enemies.AddDynamicEntity(enemy);
                }

                currentLayer++;
            }
        }

        public void RenderFormation() {
            Enemies.RenderEntities();
        }
    }
}