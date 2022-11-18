using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CodeLouisvilleUnitTestProject
{
    public class SemiTruck : Vehicle
    {
        public List<CargoItem> Cargo { get; private set; } = new();

        /// <summary>
        /// Creates a new SemiTruck that always has 18 Tires
        /// </summary>
        public SemiTruck()
        {
            NumberOfTires =  18;
        }

        /// <summary>
        /// Adds the passed CargoItem to the Cargo
        /// </summary>
        /// <param name="item">The CargoItem to add</param>
        public void LoadCargo(CargoItem item)
        {
            Cargo.Add(item);
        }
            
        /// <summary>
        /// Attempts to remove the first item with the passed name from the Cargo and return it
        /// </summary>
        /// <param name="name">The name of the CargoItem to attempt to remove</param>
        /// <returns>The removed CargoItem</returns>
        /// <exception cref="ArgumentException">Thrown if no CargoItem in the Cargo matches the passed name</exception>
        public CargoItem UnloadCargo(string name)
        {
           try
            {
                CargoItem itemToRemove = Cargo.Where(x => x.Name == name).First();
                Cargo.Remove(itemToRemove);
                return itemToRemove;
            }
            catch (Exception cref)
            {
                throw new ArgumentException(name + " not found.", cref);
            }
        }

        /// <summary>
        /// Returns all CargoItems with the exact name passed. If no CargoItems have that name, returns an empty List.
        /// </summary>
        /// <param name="name">The name to match</param>
        /// <returns>A List of CargoItems with the exact name passed</returns>
        public List<CargoItem> GetCargoItemsByName(string name)
        {
            List<CargoItem> returnNameList = new ();
            foreach (var item in Cargo)
            {
                if (item.Name == name)
                {
                    returnNameList.Add(item);
                }
            }
            return returnNameList;
        }

        /// <summary>
        ///  Returns all CargoItems who have a description containing the passed description. If no CargoItems have that name, returns an empty list.
        /// </summary>
        /// <param name="description">The partial description to match</param>
        /// <returns>A List of CargoItems with a description containing the passed description</returns>
        public List<CargoItem> GetCargoItemsByPartialDescription(string description)
        {
            List<CargoItem> returnDescList = new ();
            foreach (var item in Cargo)
            {
                if (item.Description.Contains(description))
                {
                    returnDescList.Add(item);
                }
            }
            return returnDescList;
        }

        /// <summary>
        /// Get the number of total items in the Cargo.
        /// </summary>
        /// <returns>An integer representing the sum of all Quantity properties on all CargoItems</returns>
        public int GetTotalNumberOfItems()
        {
            int itemQuantity = 0;
            foreach (var item in Cargo)
            {
                itemQuantity += item.Quantity;
            }
            return itemQuantity;
        }

        public void LoadCargoList(List<CargoItem> cargoList)
        {
            Cargo = cargoList;
        }
    }
}
