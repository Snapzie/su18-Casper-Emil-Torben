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
            FileReader fr = new FileReader();
            
            Level lvl =
                fr.ReadFile(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"));
            Assert.AreEqual("SHORT -N- SWEET", lvl.Name);
            lvl = fr.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            Assert.AreEqual("THE BEACH", lvl.Name);
        }

        [Test]
        public void TestLevelPlatforms() {
            FileReader fr = new FileReader();
            
            Level lvl =
                fr.ReadFile(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"));
            Assert.AreEqual(lvl.Platforms, new List<char> {'1'});
            lvl = fr.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            Assert.AreEqual(lvl.Platforms, new List<char> {'J', 'i', 'r'});
        }

        [Test]
        public void TestLevelCostomers() {
            FileReader fr = new FileReader();
            
            Level lvl =
                fr.ReadFile(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"));
            Assert.AreEqual(lvl.Customers, new List<string> {"Alice 10 1 ^J 10 100"});
            lvl = fr.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            Assert.AreEqual(lvl.Customers, new List<string> {"Bob 10 J r 10 100", "Carol 30 r ^ 10 100"});
        }
        
    }
}