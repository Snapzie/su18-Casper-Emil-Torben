using DIKUArcade.Entities;
using Galaga_Exercise_2.GalagaEntities;
using System.Collections.Generic;
using DIKUArcade.Graphics;


namespace Galaga_Exercise_2.Squadrons {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }
        
        void CreateEnemies(List<Image> enemyStrides);

        void RenderFormation();
    }
}
    