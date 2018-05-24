using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Timers;
using NUnit.Framework;
using SpaceTaxi_1.Customers;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.MockUps;

namespace Tests {
    [TestFixture]
    public class CustomerTests2 {
        private CustomerTranslator customerTranslator;
        private char[][] levelChars;
        private List<string> customerList;
        private long nowTime;
        private long startTime;
        private long timeSpan;

        [SetUp]
        public void SetUp() {
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
            customerTranslator = new CustomerTranslator();
            FileReader fr = new FileReader();
            levelChars =
                fr.ReadFile(new FileStream(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"), FileMode.Open))
                    .LevelLayout;
            customerList = new List<string>() {
                "Alice 10 1 ^J 10 100",
                "Bob 10 J r 105 100",
                "Carol 302 r ^ 20 100",
                "John 20 r ^1 10 200"
            };
        }


        [Test]
        public void TestOnTime() {
            while ((nowTime = StaticTimer.GetElapsedMilliseconds()) - startTime < timeSpan + 0) {
                Customer[] customers =
                    customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
                Assert.AreEqual(customers[0].CalculatePoints(), customers[0].Points);
                Assert.AreEqual(customers[1].CalculatePoints(), customers[1].Points);
                Assert.AreEqual(customers[2].CalculatePoints(), customers[2].Points);
                Assert.AreEqual(customers[3].CalculatePoints(), customers[3].Points);
            }
        }
        [Test]
        public void 



    }
}
