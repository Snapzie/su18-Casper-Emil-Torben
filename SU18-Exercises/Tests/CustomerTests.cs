using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using NUnit.Framework;
using SpaceTaxi_1.Customers;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.MockUps;

namespace Tests {
    [TestFixture]
    public class CustomerTests {
        private CustomerTranslator customerTranslator;
        private char[][] levelChars;
        private List<string> customerList;
        private double nowTime;
        private double startTime;
        private long timeSpan;

        [SetUp]
        public void SetUp() {
            MockUpImage mockUpImage = new MockUpImage();
            Entity entity = new Entity(null, mockUpImage);
            Customer customer = new Customer("Bob", 2, 'J', "r", 105, 100, entity);
        }


        [Test]
        // Because no time passes, the full amount of points will be given, which in this
        // case is equal to 100 points
            public void TestOnTime() {
            MockUpImage mockUpImage = new MockUpImage();
            Entity entity = new Entity(null, mockUpImage);
            var Points = 100; 
            Customer customer = new Customer("Bob", 2, 'J', "r", 1, 100, entity);
            Assert.AreEqual(customer.CalculatePoints(), customer.Points);
        }

        [Test]
        // Because five seconds passes, which is more than the established 
        // timeToDropOff (1 second), a certain amount of points will be withdrawn from the
        // amount of points given by this costumer (100).
            public void FiveSecondsLate() {
            MockUpImage mockUpImage = new MockUpImage();
            Entity entity = new Entity(null, mockUpImage);
            Customer customer = new Customer("Bob", 2, 'J', "r", 1, 100, entity);
            var Points = 100;
            startTime = StaticTimer.GetElapsedSeconds();
            nowTime = 0.0;
            customer.pickUpTime = startTime;
            while((nowTime = StaticTimer.GetElapsedSeconds()) < 5) {}
            Assert.Less(customer.CalculatePoints(), Points);
        }

        [Test]
        // Seeing as how we don't allow for bonus points, dropping off the customer 
        // early will not result in an extra amount of points given.
        // This is tested by setting pickUpTime to an arbitary larger value
        // than the amount of time elapsed. 
            public void FiveSecondsEarly() {
            MockUpImage mockUpImage = new MockUpImage();
            Entity entity = new Entity(null, mockUpImage);
            Customer customer = new Customer("Bob", 2, 'J', "r", 1, 100, entity);
            var Points = 100;
            customer.pickUpTime = 20;
            Assert.AreEqual(customer.CalculatePoints(), Points);
        }
    }
        
    
    
}
