using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using NUnit.Framework;
using SpaceTaxi_1.LevelParsing;

namespace SpaceTaxiTests {
    [TestFixture]
    public class UnitTest1 {
        [Test]
        public void TestMethod1() {
            Entity e = EntityCreator.CreateEntity(0, 0, "aspargus-edge-bottom.png");
            Assert.AreEqual(e.Shape.Position, new Vec2D(0, 0));
        }
    }
}
