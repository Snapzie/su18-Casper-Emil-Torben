using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga_Exercise_3._1.GalagaEntities;

namespace Galaga_Exercise_3._1.Squadrons {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }
        
        void CreateEnemies(List<Image> enemyStrides);

        void RenderFormation();
    }
}
    