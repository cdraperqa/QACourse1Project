using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Xml.Linq;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class SemiTruckTests
    {
        private List<CargoItem> TestCargoList { get; set; }
        
        //Verify that the SemiTruck constructor creates a new SemiTruck
        //object which is also a Vehicle and has 18 wheels. Verify that the
        //Cargo property for the newly created SemiTruck is a List of
        //CargoItems which is empty, but not null.
        [Fact]
        public void NewSemiTruckIsAVehicleAndHas18TiresAndEmptyCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new ();

            //act

            //assert
            using (new AssertionScope())
            {
                semiTruck.GetType().IsSubclassOf(typeof(Vehicle)).Should().Be(true);
                semiTruck.NumberOfTires.Should().Be(18);
                semiTruck.Cargo.Should().BeEmpty();
                semiTruck.Cargo.Should().NotBeNull();
            }
        }

        //Verify that adding a CargoItem using LoadCargo does successfully add
        //that CargoItem to the Cargo. Confirm both the existence of the new
        //CargoItem in the Cargo and also that the count of Cargo increased to 1.
        [Fact]
        public void LoadCargoTest()
        {
            //arrange
            CargoItem cargoToAdd = new ("packing tape", "moving supplies", 50);
            SemiTruck semiTruck = new ();

            //act
            semiTruck.LoadCargo(cargoToAdd);

            //assert
            using (new AssertionScope())
            {
                semiTruck.Cargo.Count.Should().Be(1);
                semiTruck.Cargo.Contains(cargoToAdd);
            }
        }

        //Verify that unloading a cargo item that is in the Cargo does
        //remove it from the Cargo and return the matching CargoItem
        [Fact]
        public void UnloadCargoWithValidCargoTest()
        {
            //arrange
            CargoItem cargoToRemove = new ("blue boxes", "moving supplies", 25);
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            CargoItem unloadedItem = semiTruck.UnloadCargo(cargoToRemove.Name);

            //assert
            using (new AssertionScope())
            {
                semiTruck.Cargo.Should().NotContain(cargoToRemove);
                unloadedItem.Equals(cargoToRemove);
            }
        }

        //Verify that attempting to unload a CargoItem that does not
        //appear in the Cargo throws a System.ArgumentException
        [Fact]
        public void UnloadCargoWithInvalidCargoTest()
        {
            //arrange
            Exception exception = new();
            CargoItem cargoToRemove = new ("packing tape", "moving supplies", 50);
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            try
            {
                semiTruck.UnloadCargo(cargoToRemove.Name);
            }
            catch (ArgumentException ex)
            {
                exception = ex;
            }

            //assert
            using (new AssertionScope())
            {
                exception.GetType().Name.Should().Be("ArgumentException");
                exception.Message.Should().Be("packing tape not found.");
            }
        }

        //Verify that getting cargo items by name returns all items
        //in Cargo with that name.
        [Fact]
        public void GetCargoItemsByNameWithValidName()
        {
            //arrange
            string removeCargoName = "purple boxes";
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            List<CargoItem> itemsFound = semiTruck.GetCargoItemsByName(removeCargoName);

            //assert
            using (new AssertionScope())
            {
                itemsFound.Count.Should().Be(2);
                foreach (CargoItem item in itemsFound)
                {
                    item.Name.Should().Be(removeCargoName);
                }
            }
        }

        //Verify that searching the Carto list for an item that does not
        //exist returns an empty list
        [Fact]
        public void GetCargoItemsByNameWithInvalidName()
        {
            //arrange
            string removeCargoName = "packing tape";
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            List<CargoItem> itemsRemoved = semiTruck.GetCargoItemsByName(removeCargoName);

            //assert
            itemsRemoved.Count.Should().Be(0);
        }

        //Verify that searching the Cargo list by description for an item
        //that does exist returns all matched items that contain that description.
        [Fact]
        public void GetCargoItemsByPartialDescriptionWithValidDescription()
        {
            //arrange
            string removeCargoDesc = "supplies";
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            List<CargoItem> itemsFound = semiTruck.GetCargoItemsByPartialDescription(removeCargoDesc);

            //assert
            using (new AssertionScope())
            {
                itemsFound.Count.Should().Be(7);
                foreach (CargoItem cargoItem in itemsFound)
                {
                    cargoItem.Description.Should().Contain(removeCargoDesc);
                }
            }
        }

        //Verify that searching the Carto list by description for an item
        //that does not exist returns an empty list
        [Fact]
        public void GetCargoItemsByPartialDescriptionWithInvalidDescription()
        {
            //arrange
            string removeCargoDesc = "produce";
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            List<CargoItem> itemsFound = semiTruck.GetCargoItemsByPartialDescription(removeCargoDesc);

            //assert
            itemsFound.Count.Should().Be(0);
        }

        //Verify that the method returns the sum of all quantities of all
        //items in the Cargo
        [Fact]
        public void GetTotalNumberOfItemsReturnsSumOfAllQuantities()
        {
            //arrange
            SemiTruck semiTruck = new ();
            this.CreateTestCargoList();
            semiTruck.LoadCargoList(TestCargoList);

            //act
            int cargoSum = semiTruck.GetTotalNumberOfItems();

            //assert
            cargoSum.Should().Be(200);
        }

        private void CreateTestCargoList()
        {
            TestCargoList = new List<CargoItem>
            {
                new CargoItem("purple boxes", "moving supplies", 27),
                new CargoItem("blue boxes", "moving supplies", 25),
                new CargoItem("red boxes", "moving supplies", 10),
                new CargoItem("green boxes", "moving supplies", 30),
                new CargoItem("purple boxes", "moving supplies", 23),
                new CargoItem("bleach", "cleaning supplies", 15),
                new CargoItem("mops", "cleaning supplies", 20),
                new CargoItem("white paint", "home decor", 20),
                new CargoItem("black paint", "home decor", 20),
                new CargoItem("grey paint", "home decor", 10)
            };
        }
    }
}
