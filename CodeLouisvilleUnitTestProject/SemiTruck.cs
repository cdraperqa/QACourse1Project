using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CodeLouisvilleUnitTestProject
{
    public class SemiTruck : Vehicle
    {
        public List<CargoItem> Cargo { get; private set; } = new List<CargoItem>();

        /// <summary>
        /// Creates a new SemiTruck that always has 18 Tires
        /// </summary>
        public SemiTruck()
        {
            //YOUR CODE HERE:  --Trent says should be 1 line
            NumberOfTires =  18;
            //Cargo = new List<CargoItem>();
        }

        /// <summary>
        /// Adds the passed CargoItem to the Cargo
        /// </summary>
        /// <param name="item">The CargoItem to add</param>
        public void LoadCargo(CargoItem item)
        {
            //YOUR CODE HERE -- Trent says should be 1 line
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
                //var itemToRemove = Cargo.First(Name == name);
                CargoItem itemToRemove = Cargo.Where(x => x.Name == name).First();
                Cargo.Remove(itemToRemove);
                return itemToRemove;
            }
            catch (Exception cref)
            //catch (ArgumentException cref)
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
            List<CargoItem> returnNameList = new List<CargoItem>();
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
            List<CargoItem> returnDescList = new List<CargoItem>();
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
        public void InitializeCargoList()
        {
            Cargo.Add(new CargoItem("purple boxes", "moving supplies", 27));
            Cargo.Add(new CargoItem("blue boxes", "moving supplies", 25));
            Cargo.Add(new CargoItem("red boxes", "moving supplies", 10));
            Cargo.Add(new CargoItem("green boxes", "moving supplies", 30));
            Cargo.Add(new CargoItem("purple boxes", "moving supplies", 23));
            Cargo.Add(new CargoItem("bleach", "cleaning supplies", 15));
            Cargo.Add(new CargoItem("mops", "cleaning supplies", 20));
            Cargo.Add(new CargoItem("white paint", "home decor", 20));
            Cargo.Add(new CargoItem("black paint", "home decor", 20));
            Cargo.Add(new CargoItem("grey paint", "home decor", 10));
        }
    }
}
