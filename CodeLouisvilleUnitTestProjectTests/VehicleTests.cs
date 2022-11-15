using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Threading.Tasks.Dataflow;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class VehicleTests
    {

        //Verify the parameterless constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to their default values.
        [Fact]
        public void VehicleParameterlessConstructorTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle();

            //act

            //assert
            using (new AssertionScope())
            {
                vehicle.NumberOfTires.Should().Be(0);
                vehicle.GasTankCapacity.Should().Be(0);
                vehicle.Make.Should().Be("");
                vehicle.Model.Should().Be("");
                vehicle.MilesPerGallon.Should().Be(0);
            }
        }

        //Verify the parameterized constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to the provided values.
        [Fact]
        public void VehicleConstructorTest()
        {
            //arrange
            int numberOfTires = 4;
            double gasTankCapacity = 12;
            string make = "Nissan";
            string model = "Sentry";
            double milesPerGallon = 25;
            Vehicle vehicle = new Vehicle(numberOfTires, gasTankCapacity, make, model, milesPerGallon);

            //act

            //assert
            using (new AssertionScope())
            {
                vehicle.NumberOfTires.Should().Be(numberOfTires);
                vehicle.GasTankCapacity.Should().Be(gasTankCapacity);
                vehicle.Make.Should().Be(make);
                vehicle.Model.Should().Be(model);
                vehicle.MilesPerGallon.Should().Be(milesPerGallon);
            }
        }

        //Verify that the parameterless AddGas method fills the gas tank
        //to 100% of its capacity
        [Fact]
        public void AddGasParameterlessFillsGasToMax()
        {
            //arrange
            int numberOfTires = 4;
            double gasTankCapacity = 12;
            string make = "Nissan";
            string model = "Sentry";
            double milesPerGallon = 25;
            Vehicle vehicle = new Vehicle(numberOfTires, gasTankCapacity, make, model, milesPerGallon);

            //act
            vehicle.AddGas();

            //assert
            vehicle.GasLevel.Should().Be("100%");
        }

        //Verify that the AddGas method with a parameter adds the
        //supplied amount of gas to the gas tank.
        [Fact]
        public void AddGasWithParameterAddsSuppliedAmountOfGas()
        {
            //arrange
            int numberOfTires = 4;
            double gasTankCapacity = 12;
            string make = "Nissan";
            string model = "Sentry";
            double milesPerGallon = 25;
            Vehicle vehicle = new Vehicle(numberOfTires, gasTankCapacity, make, model, milesPerGallon);

            //act
            vehicle.AddGas(3);

            //assert
            vehicle.GasLevel.Should().Be("25%");
        }

        //Verify that the AddGas method with a parameter will throw
        //a GasOverfillException if too much gas is added to the tank.
        [Fact]
        public void AddingTooMuchGasThrowsGasOverflowException()
        {
            //arrange
            int numberOfTires = 4;
            double gasTankCapacity = 12;
            string make = "Nissan";
            string model = "Sentry";
            double milesPerGallon = 25;
            float gasToAdd = 15;
            string exceptionMessage = "";

            //act
            Vehicle vehicle = new Vehicle(numberOfTires, gasTankCapacity, make, model, milesPerGallon);
            try
            {
                vehicle.AddGas(gasToAdd);
            }
            catch(Exception exceptCaught)
            {
                exceptionMessage = exceptCaught.ToString();
            }

            //assert
            exceptionMessage.Contains("Unable to add " + gasToAdd + " gallons to tank because it would exceed the capacity of " + gasTankCapacity + " gallons");
        }

        //Using a Theory (or data-driven test), verify that the GasLevel
        //property returns the correct percentage when the gas level is
        //at 0%, 25%, 50%, 75%, and 100%.
        [Theory]
        [InlineData("0%", 0)]
        [InlineData("25%", 2.5)]
        [InlineData("50%", 5)]
        [InlineData("75%", 7.5)]
        [InlineData("100%", 10)]
        public void GasLevelPercentageIsCorrectForAmountOfGas(string percent, float gasToAdd)
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Toyota","Camry",30);

            //act
            vehicle.AddGas(gasToAdd);

            //assert
            vehicle.GasLevel.Should().Be(percent);
        }

        /*
         * Using a Theory (or data-driven test), or a combination of several 
         * individual Fact tests, test the following functionality of the 
         * Drive method:
         *      a. Attempting to drive a car without gas returns the status 
         *      string “Cannot drive, out of gas.”.
         *      b. Attempting to drive a car with a flat tire returns 
         *      the status string “Cannot drive due to flat tire.”.
         *      c. Drive the car 10 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled, 
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      d. Drive the car 100 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled,
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      e. Drive the car until it runs out of gas. Verify that the 
         *      correct amount of gas was used, that the correct distance 
         *      was traveled, that GasLevel is correct, that MilesRemaining
         *      is correct, and that the total mileage on the vehicle is 
         *      correct. Verify that the status reports the car is out of gas.
        */
        [Theory]
        //[InlineData(0, false, 20)]
        //[InlineData(10, true, 20)]
        [InlineData(10, false, 10)]
        //public void DriveNegativeTests(params object[] yourParamsHere)
        public void DriveNegativeTests(float gasToAdd, bool hasFlatTire, double milesToDrive) 
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Toyota", "Camry", 20);

            //try
            //{
                vehicle.AddGas(gasToAdd);
                double startingMilesRemaining = vehicle.MilesRemaining;
                vehicle.HasFlatTire = hasFlatTire;
                string driveStatus = vehicle.Drive(milesToDrive);
                string gasLevel = vehicle.GasLevel;
                //double endingMileage = vehicle.Mileage;
                double endingMilesRemaining = vehicle.MilesRemaining;

            //}
            //catch (Exception exceptCaught)
            //{
            //    exceptionMessage = exceptCaught.ToString();
            //}

            //assert
            //driveStatus.Should().Be("Cannot drive, out of gas.");
            //driveStatus.Should().Be("Cannot drive due to flat tire.");
            vehicle.MilesRemaining.Should().Be(startingMilesRemaining - milesToDrive);
            vehicle.GasLevel.Should().Be("95%");
        }

        [Theory]
        [InlineData("MysteryParamValue")]
        public void DrivePositiveTests(params object[] yourParamsHere)
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert
            
        }

        //Verify that attempting to change a flat tire using
        //ChangeTireAsync will throw a NoTireToChangeException
        //if there is no flat tire.
        [Fact]
        public async Task ChangeTireWithoutFlatTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Toyota", "Camry", 20);
            vehicle.HasFlatTire = true;

            //act
            //var vehicleStatus = vehicle.ChangeTireAsync;
            
            
            //assert

        }

        //Verify that ChangeTireAsync can successfully
        //be used to change a flat tire
        [Fact]
        public async Task ChangeTireSuccessfulTest()
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //BONUS: Write a unit test that verifies that a flat
        //tire will occur after a certain number of miles.
        [Theory]
        [InlineData("MysteryParamValue")]
        public void GetFlatTireAfterCertainNumberOfMilesTest(params object[] yourParamsHere)
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }
    }
}