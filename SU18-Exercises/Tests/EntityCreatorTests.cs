using System;
using System.IO;
using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;

namespace Tests {
    [TestFixture]
    public class EntityCreatorTests {
        [SetUp]
        public void SetUp() {
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
            LevelLoader.LoadLevels();
            //Nedenstående kode fejler i run time, når der i Game forsøges at oprette et vindue til Game
            //Game g = new Game();
            //g.GameLoop();
        }
        [Test]
        public void TestPosition() {
            //Entity e = EntityCreator.CreateEntity(0, 0, "aspargus-edge-bottom.png");
            //TestContext.CurrentContext.WorkDirectory
            /*Entity e = EntityCreator.CreateEntity(0, 0, "aspargus-edge-bottom.png");
            Assert.AreEqual(e.Shape.Position, new Vec2D(0, 0));
            e = EntityCreator.CreateEntity(23, 40, "aspargus-edge-bottom.png");
            Assert.AreEqual(e.Shape.Position, new Vec2D(1, 1));
            e = EntityCreator.CreateEntity(23, 20, "aspargus-edge-bottom.png");
            Assert.AreEqual(e.Shape.Position, new Vec2D(1, 0.5f));*/
        }
    }
}