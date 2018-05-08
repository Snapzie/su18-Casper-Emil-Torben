using System;
using System.IO;
using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.MockUps;

namespace Tests {
    [TestFixture]
    public class EntityCreatorTests {
        [SetUp]
        public void SetUp() {
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
            //LevelLoader.LoadLevels();
            //Nedenstående kode fejler i run time, når der i Game forsøges at oprette et vindue til Game
            //Game g = new Game();
            //g.GameLoop();
        }
        [Test]
        public void TestPosition() {
            IBaseImage img = new MockUpImage();
            Entity e = EntityCreator.CreateEntity(0, 0, img);
            Vec2F expected = new Vec2F(0, 1);
            //Check results are equal, within margin
            Assert.LessOrEqual(e.Shape.Position.X - expected.X, 0.01); 
            Assert.LessOrEqual(e.Shape.Position.Y - expected.Y - 1f / 23, 0.01);
            e = EntityCreator.CreateEntity(23, 40, img);
            expected = new Vec2F(1, 0);
            Assert.LessOrEqual(e.Shape.Position.X - expected.X, 0.01); 
            Assert.LessOrEqual(e.Shape.Position.Y - expected.Y - 1f / 23, 0.01);
            e = EntityCreator.CreateEntity(23, 20, img);
            expected = new Vec2F(1, 0);
            Assert.LessOrEqual(e.Shape.Position.X - expected.X, 0.01); 
            Assert.LessOrEqual(e.Shape.Position.Y - expected.Y - 1f / 23, 0.01);
        }
    }
}