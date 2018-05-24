using System;
using System.Collections.Generic;
using System.IO;
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
        [SetUp]
        public void SetUp() {
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
            customerTranslator = new CustomerTranslator();
            FileReader fr = new FileReader();
            levelChars =
                fr.ReadFile(new FileStream(Path.Combine("..", "..", "Levels", "short-n-sweet.txt"), FileMode.Open)).LevelLayout;
            customerList = new List<string>() {
                "Alice 10 1 ^J 10 100",
                "Bob 10 J r 105 100",
                "Carol 302 r ^ 20 100",
                "John 20 r ^1 10 200"
            };
        }
        
        [Test]
        public void TestWithEmptyList() {
            Customer[] customers = 
                customerTranslator.MakeCustomers(customerList.GetRange(0,0), levelChars, new MockUpImage());
            Assert.AreEqual(customers.Length, 0);
        }
        
        [Test]
        public void TestWithOneElement() {
            Customer[] customers = 
                customerTranslator.MakeCustomers(customerList.GetRange(0,1), levelChars, new MockUpImage());
            Assert.AreEqual(customers.Length, 1);
        }
        
        [Test]
        public void TestWithMultipleElements() {
            Customer[] customers = 
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers.Length, customerList.Count);
        }

        [Test]
        public void TestName() {
            Customer[] customers =
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers[0].Name, "Alice");
            Assert.AreEqual(customers[1].Name, "Bob");
            Assert.AreEqual(customers[2].Name, "Carol");
            Assert.AreEqual(customers[3].Name, "John");
        }

        [Test]
        public void TestSpawTime() {
            Customer[] customers =
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers[0].SpawnTime, 10);
            Assert.AreEqual(customers[1].SpawnTime, 10);
            Assert.AreEqual(customers[2].SpawnTime, 302);
            Assert.AreEqual(customers[3].SpawnTime, 20);
        }
        
        [Test]
        public void TestSpawPlatform() {
            Customer[] customers =
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers[0].SpawnPlatform, '1');
            Assert.AreEqual(customers[1].SpawnPlatform, 'J');
            Assert.AreEqual(customers[2].SpawnPlatform, 'r');
            Assert.AreEqual(customers[3].SpawnPlatform, 'r');
        }
        
        [Test]
        public void TestDestinationPlatform() {
            Customer[] customers =
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers[0].DestinationPlatform, "^J");
            Assert.AreEqual(customers[1].DestinationPlatform, "r");
            Assert.AreEqual(customers[2].DestinationPlatform, "^");
            Assert.AreEqual(customers[3].DestinationPlatform, "^1");
        }
        
        [Test]
        public void TestTimeToDropOff() {
            Customer[] customers =
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers[0].TimeToDropOff, 10);
            Assert.AreEqual(customers[1].TimeToDropOff, 105);
            Assert.AreEqual(customers[2].TimeToDropOff, 20);
            Assert.AreEqual(customers[3].TimeToDropOff, 10);
        }
        
        [Test]
        public void TestPoints() {
            Customer[] customers =
                customerTranslator.MakeCustomers(customerList, levelChars, new MockUpImage());
            Assert.AreEqual(customers[0].Points, 100);
            Assert.AreEqual(customers[1].Points, 100);
            Assert.AreEqual(customers[2].Points, 100);
            Assert.AreEqual(customers[3].Points, 200);
        }
        
    }
}