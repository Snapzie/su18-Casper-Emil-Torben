using NUnit.Framework;
using System;
using SpaceTaxi_1.LevelParsing;
using DIKUArcade.Entities;

namespace Tests
{
    [TestFixture()]
    public class LevelCreatorTests {
        private LevelsKeeper levelKeeper;
        private Level level;
        private LevelLoader levelLoader;
        private LevelCreator levelCreator;
        
        [SetUp]
        public void SetUp() {
            levelLoader = new LevelLoader();
            levelLoader.LoadLevels();
            levelCreator = new LevelCreator();
        }
        
        [Test()]
        public void TestNumberOfPlatforms() {
            // 13 platforms are counted in the txt-file
            EntityContainer<Entity>[] entityContainers = levelCreator.CreateLevel(0);
            Assert.AreEqual(13, entityContainers[0].CountEntities());
        
        }
        [Test()]
        public void TestNumberOfBlocks() {
            // 173 blocks are counted in the text-file 
            EntityContainer<Entity>[] entityContainers = levelCreator.CreateLevel(0);
            Assert.AreEqual(173, entityContainers[1].CountEntities());
        
        }
    }
}
