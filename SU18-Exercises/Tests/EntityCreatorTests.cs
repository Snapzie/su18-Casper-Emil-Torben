using System;
using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using SpaceTaxi_1.LevelParsing;

namespace Tests {
    [TestFixture]
    public class EntityCreatorTests {
        [Test]
        public void TestPosition() {
            Entity e = EntityCreator.CreateEntity(0, 0, "aspargus-edge-bottom.png");
            Assert.AreEqual(e.Shape.Position, new Vec2D(0, 0));
            //e = EntityCreator.CreateEntity(23, 40, "aspargus-edge-bottom.png");
            //Assert.AreEqual(e.Shape.Position, new Vec2D(1, 1));
            //e = EntityCreator.CreateEntity(23, 20, "aspargus-edge-bottom.png");
            //Assert.AreEqual(e.Shape.Position, new Vec2D(1, 0.5f));
        }
    }
}