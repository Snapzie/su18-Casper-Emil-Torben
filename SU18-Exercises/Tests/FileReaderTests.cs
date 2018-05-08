using System;
using System.IO;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using System.Collections.Generic;

namespace Tests {
    [TestFixture]
    public class FileReaderTests {      
        [SetUp]
        public void SetUp() {
            //Sets working dirrectory in order to find assets
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
        }

        [Test]
        public void TestLevelName() {
            Level lvl =
                FileReader.ReadFile(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"));
            Assert.AreEqual("SHORT -N- SWEET", lvl.Name);
            lvl = FileReader.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            Assert.AreEqual("THE BEACH", lvl.Name);
        }

        [Test]
        public void TestLevelPlatforms() {
            Level lvl =
                FileReader.ReadFile(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"));
            Assert.AreEqual(lvl.Platforms, new List<char> {'1'});
            lvl = FileReader.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            Assert.AreEqual(lvl.Platforms, new List<char> {'J', 'i', 'r'});
        }

        [Test]
        public void TestLevelCostomers() {
            Level lvl =
                FileReader.ReadFile(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"));
            Assert.AreEqual(lvl.Customers, new List<string> {"Alice 10 1 ^J 10 100"});
            lvl = FileReader.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            Assert.AreEqual(lvl.Customers, new List<string> {"Bob 10 J r 10 100", "Carol 30 r ^ 10 100"});
        }
        
    }
}