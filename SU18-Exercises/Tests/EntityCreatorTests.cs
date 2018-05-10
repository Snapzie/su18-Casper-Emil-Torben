﻿using System;
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
        }
        [Test]
        public void TestPosition() {
            EntityCreator ec = new EntityCreator();
            
            IBaseImage img = new MockUpImage();
            Entity e = ec.CreateEntity(0, 0, img);
            Vec2F expected = new Vec2F(0, 1);
            //Check results are equal, within margin
            Assert.LessOrEqual(e.Shape.Position.X - expected.X, 0.01); 
            Assert.LessOrEqual(e.Shape.Position.Y - expected.Y - 1f / 23, 0.01);
            e = ec.CreateEntity(23, 40, img);
            expected = new Vec2F(1, 0);
            Assert.LessOrEqual(e.Shape.Position.X - expected.X, 0.01); 
            Assert.LessOrEqual(e.Shape.Position.Y - expected.Y - 1f / 23, 0.01);
            e = ec.CreateEntity(23, 20, img);
            expected = new Vec2F(1, 0);
            Assert.LessOrEqual(e.Shape.Position.X - expected.X, 0.01); 
            Assert.LessOrEqual(e.Shape.Position.Y - expected.Y - 1f / 23, 0.01);
        }
    }
}