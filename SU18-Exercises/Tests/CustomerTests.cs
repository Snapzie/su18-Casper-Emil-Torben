using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using NUnit.Framework;
using SpaceTaxi_1.Customers;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.MockUps;

namespace Tests {
    [TestFixture]
    public class CustomerTests {
        private double nowTime;
        private double startTime;
        private Customer customer;
        private int maxPoints;

        [SetUp]
        public void SetUp() {
            MockUpImage mockUpImage = new MockUpImage();
            Entity entity = new Entity(null, mockUpImage);
            maxPoints = 100;
            customer = new Customer("Bob", 2, 'J', "r", 1, maxPoints, entity);
        }


        [Test]
        // Because no time passes, the full amount of points will be given, which in this
        // case is equal to 100 points
        public void TestOnTime() {
            customer.pickUpTime = StaticTimer.GetElapsedSeconds();
            Assert.AreEqual(customer.CalculatePoints(), maxPoints);
        }

        [Test]
        // Because five seconds passes, which is more than the established 
        // timeToDropOff (1 second), a certain amount of points will be withdrawn from the
        // amount of points given by this costumer (100).
        public void FiveSecondsLate() {
            startTime = StaticTimer.GetElapsedSeconds();
            customer.pickUpTime = startTime;
            while((nowTime = StaticTimer.GetElapsedSeconds()) - startTime < 5) {}
            Assert.Less(customer.CalculatePoints(), maxPoints);
        }
        
    }
        
    
    
}
