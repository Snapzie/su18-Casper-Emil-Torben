using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIKUArcade.Entities;
using SpaceTaxi_1.Customers;

namespace SpaceTaxi_1.LevelParsing {
    public class Level {
        public char[][] LevelLayout { get; }
        public string Name { get; }
        public List<char> Platforms { get; }
        public Dictionary<char, string> Decoder { get; }
        public EntityContainer Customers { get; set; }

        /// <summary>
        /// Constructor for a Level object which is used to hold information about the level
        /// </summary>
        /// <param name="levelLayout">Jagged char array to hold each character the level consists of</param>
        /// <param name="name">Name of the level</param>
        /// <param name="platforms">The chars in the level which represents platforms</param>
        /// <param name="decoder">A dictionary mapping the chars the level consists of with the name of the image file the char represents</param>
        /// <param name="customers">A list of customers in the level</param>
        public Level(char[][] levelLayout, string name, List<char> platforms, Dictionary<char, string> decoder) {
            LevelLayout = levelLayout;
            Name = name;
            Platforms = platforms;
            Decoder = decoder;
        }

        public void AddCustomer(Entity entity) {
            Customers.AddStationaryEntity((StationaryShape)entity.Shape, entity.Image);
        }

        public void RemoveCustomer(Entity entity) {
            entity.DeleteEntity();
            ///CustomerIterator kaldes for at iterere over Customers
            /// for at fjerne den pågældende customers entity i Customers
            Customers.Iterate(CustomerIterator);
            
        }
        /// <summary>
        /// Empty method to ensure iteration over customer
        /// </summary>
        /// <param name="customer"></param>
        private void CustomerIterator(Entity customer) {
            
        }
    }
}
