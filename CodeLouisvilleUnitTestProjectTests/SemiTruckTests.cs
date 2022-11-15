using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class SemiTruckTests
    {

        //Verify that the SemiTruck constructor creates a new SemiTruck
        //object which is also a Vehicle and has 18 wheels. Verify that the
        //Cargo property for the newly created SemiTruck is a List of
        //CargoItems which is empty, but not null.
        [Fact]
        public void NewSemiTruckIsAVehicleAndHas18TiresAndEmptyCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();

            //act

            //assert
            using (new AssertionScope())
            {
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
            CargoItem cargoToAdd = new CargoItem("packing tape", "moving supplies", 50);

            //act
            SemiTruck semiTruck = new SemiTruck();
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
            CargoItem cargoToRemove = new CargoItem("blue boxes", "moving supplies", 25);

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
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
            string exceptionMessage = "";
            CargoItem cargoToRemove = new CargoItem("packing tape", "moving supplies", 50);

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
            try
            {
                semiTruck.UnloadCargo(cargoToRemove.Name);
            }
            catch (ArgumentException e)
            {
                exceptionMessage = e.Message;
            }

            //assert
            exceptionMessage.Should().Be("packing tape not found.");

        }

        //Verify that getting cargo items by name returns all items
        //in Cargo with that name.
        [Fact]
        public void GetCargoItemsByNameWithValidName()
        {
            //arrange
            string removeCargoName = "purple boxes";

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
            List<CargoItem> itemsRemoved = semiTruck.GetCargoItemsByName(removeCargoName);

            //assert
            using (new AssertionScope())
            {
                foreach (CargoItem cargoItem in itemsRemoved)
                {
                    cargoItem.Name.Should().Be(removeCargoName);
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

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
            List<CargoItem> itemsRemoved = semiTruck.GetCargoItemsByName(removeCargoName);

            //assert
            Assert.Empty(itemsRemoved);
        }

        //Verify that searching the Cargo list by description for an item
        //that does exist returns all matched items that contain that description.
        [Fact]
        public void GetCargoItemsByPartialDescriptionWithValidDescription()
        {
            //arrange
            string removeCargoDesc = "supplies";

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
            List<CargoItem> itemsFound = semiTruck.GetCargoItemsByPartialDescription(removeCargoDesc);

            //assert
            using (new AssertionScope())
            {
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

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
            List<CargoItem> itemsFound = semiTruck.GetCargoItemsByPartialDescription(removeCargoDesc);

            //assert
            Assert.Empty(itemsFound);
        }

        //Verify that the method returns the sum of all quantities of all
        //items in the Cargo
        [Fact]
        public void GetTotalNumberOfItemsReturnsSumOfAllQuantities()
        {
            //arrange
            //190

            //act
            SemiTruck semiTruck = new SemiTruck();
            semiTruck.InitializeCargoList();
            int cargoSum = semiTruck.GetTotalNumberOfItems();

            //assert
            cargoSum.Should().Be(200);
        }
    }
}
