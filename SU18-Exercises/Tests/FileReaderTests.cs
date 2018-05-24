﻿using System;
using System.IO;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using System.Collections.Generic;

namespace Tests {
    [TestFixture]
    public class FileReaderTests {
        private FileReader fileReader;
        private FileStream shortNSweetStream;
        private FileStream beachStream;
        [SetUp]
        public void SetUp() {
            //Sets working dirrectory in order to find assets
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
            fileReader = new FileReader();
            shortNSweetStream = new FileStream(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"),
                FileMode.Open);
            beachStream = new FileStream(Path.Combine("..", "..", "Levels", "the-beach.txt"),
                FileMode.Open);
        }

        [Test]
        public void TestLevelName() {
            Level lvl =
                fileReader.ReadFile(shortNSweetStream);
            Assert.AreEqual("SHORT -N- SWEET", lvl.Name);
            lvl = fileReader.ReadFile(beachStream);
            Assert.AreEqual("THE BEACH", lvl.Name);
        }

        [Test]
        public void TestLevelPlatforms() {
            Level lvl =
                fileReader.ReadFile(shortNSweetStream);
            Assert.AreEqual(lvl.Platforms, new List<char> {'1'});
            lvl = fileReader.ReadFile(beachStream);
            Assert.AreEqual(lvl.Platforms, new List<char> {'J', 'i', 'r'});
        }

        [Test]
        public void TestLevelCostomers() {
            Level lvl =
                fileReader.ReadFile(shortNSweetStream);
            Assert.AreEqual(lvl.Customers, new List<string> {"Alice 10 1 ^J 10 100"});
            lvl = fileReader.ReadFile(beachStream);
            Assert.AreEqual(lvl.Customers, new List<string> {"Bob 10 J r 10 100", "Carol 30 r ^ 10 100"});
        }
        
        [Test]
        public void TestLevelChars() {
            Level lvl =
                fileReader.ReadFile(shortNSweetStream);
            Assert.AreEqual(lvl.LevelLayout[0][0], '%');
            Assert.AreEqual(lvl.LevelLayout[0][39], '#');
            lvl = fileReader.ReadFile(beachStream);
            Assert.AreEqual(lvl.LevelLayout[16][28], 'a');
            Assert.AreEqual(lvl.LevelLayout[22][39], 'O');
        }
        
        [Test]
        public void TestLevelDecoder() {
            Level lvl =
                fileReader.ReadFile(shortNSweetStream);
            Assert.AreEqual("neptune-upper-left.png", lvl.Decoder['N']);
            Assert.AreEqual("white-square.png", lvl.Decoder['%']);
            lvl = fileReader.ReadFile(beachStream);
            Assert.AreEqual("aspargus-edge-left.png", lvl.Decoder['A']);
            Assert.AreEqual("emperor-lower-left.png", lvl.Decoder['c']);
        }
        
    }
}