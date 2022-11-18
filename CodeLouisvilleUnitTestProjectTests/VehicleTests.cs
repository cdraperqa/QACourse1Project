using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;
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
            Vehicle vehicle = new ();

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
            Vehicle vehicle = new (numberOfTires, gasTankCapacity, make, model, milesPerGallon);

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
            Vehicle vehicle = new (4, 12, "Nissan", "Sentry", 25);

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
            float gasToAdd = 3;

            //act
            Vehicle vehicle = new (4, 12, "Nissan", "Sentry", 25);
            vehicle.AddGas(gasToAdd);

            //assert
            using (new AssertionScope())
            {
                vehicle.GasRemaining.Should().Be(gasToAdd);
                vehicle.GasLevel.Should().Be("25%");
            }
        }

        //Verify that the AddGas method with a parameter will throw
        //a GasOverfillException if too much gas is added to the tank.
        [Fact]
        public void AddingTooMuchGasThrowsGasOverflowException()
        {
            //arrange
            float gasToAdd = 15;
            Exception exception = new();

            //act
            Vehicle vehicle = new (4, 12, "Nissan", "Sentry", 25);
            try
            {
                vehicle.AddGas(gasToAdd);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            
            using (new AssertionScope())
            {
                exception.GetType().Name.Should().Be("GasOverfillException");
                exception.Message.Contains($"Unable to add {gasToAdd} gallons");
                exception.Message.Contains($"capacity of {vehicle.GasTankCapacity} gallons");
            }
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
            Vehicle vehicle = new (4, 10, "Toyota","Camry",30);

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
        [InlineData(0, false, "Cannot drive, out of gas.")]
        [InlineData(10, true, "Cannot drive due to flat tire.")]
        public void DriveNegativeTests(float addGas, bool hasFlat, string expectedStatus) 
        {
            //arrange
            Vehicle vehicle = new(4, 10, "Toyota", "Camry", 20);
            vehicle.AddGas(addGas);
            vehicle.HasFlatTire = hasFlat;
            
            //act
            string status = vehicle.Drive(10);

            //assert
            status.Should().Be(expectedStatus);
        }

        [Theory]
        [InlineData(10, 10, 0.5, "95%", 190, 10)]
        [InlineData(10, 100, 5, "50%", 100, 100)]
        [InlineData(10, 300, 10, "0%", 0, 200)]
        public void DrivePositiveTests(float gasToAdd, double milesToDrive, double expectedGasUsed, string expectedGasLevel, double expectedMilesRemain, double expectedMileage)
        {
            //arrange
            Vehicle vehicle = new(4, 10, "Ford", "Escape", 20);
            vehicle.AddGas(gasToAdd);

            //act
            string status = vehicle.Drive(milesToDrive);

            //assert
            using (new AssertionScope())
            {
                if (vehicle.Mileage >= milesToDrive)
                {
                    status.Should().Contain($"Drove {milesToDrive} miles using {expectedGasUsed} gallons of gas.");
                }
                else 
                {
                    status.Should().Contain($"Drove {expectedMileage} miles, then ran out of gas.");
                }
                vehicle.GasLevel.Should().Be(expectedGasLevel);
                vehicle.MilesRemaining.Should().Be(expectedMilesRemain);
                vehicle.Mileage.Should().Be(expectedMileage);
            }
        }

       [Fact]
        public void QuickDriveTest()
        {
            Vehicle vehicle = new (4, 10, "Ford", "Escape", 20);
            bool tireAnswer = vehicle.GotFlatTire(500, 101);
            tireAnswer.Should().Be(true);
        }

        //Verify that attempting to change a flat tire using
        //ChangeTireAsync will throw a NoTireToChangeException
        //if there is no flat tire.
        [Fact]
        public async Task ChangeTireWithoutFlatTest()
        {
            //arrange
            string exceptionName = "";
            Vehicle vehicle = new (4, 10, "Toyota", "Camry", 20);

            //act
            vehicle.HasFlatTire = false;

            //assert
            try
            {
                await vehicle.ChangeTireAsync();
            }
            catch (Exception ex)
            {
                exceptionName = ex.GetType().Name.ToString();
            }
            exceptionName.Should().Be("NoTireToChangeException");
        }

        //Verify that ChangeTireAsync can successfully
        //be used to change a flat tire
        [Fact]
        public async Task ChangeTireSuccessfulTest()
        {
            //arrange
            string exceptionName = "";
            Vehicle vehicle = new(4, 10, "Toyota", "Camry", 20);

            //act
            vehicle.HasFlatTire = true;

            //assert
            try
            {
                await vehicle.ChangeTireAsync();
            }
            catch (Exception ex)
            {
                exceptionName = ex.GetType().Name.ToString();
            }
            using (new AssertionScope())
            {
                exceptionName.Should().Be("");
                vehicle.HasFlatTire.Should().Be(false);
            }
        }

        ////BONUS: Write a unit test that verifies that a flat
        ////tire will occur after a certain number of miles.
        //[Theory]
        //[InlineData("MysteryParamValue")]
        //public void GetFlatTireAfterCertainNumberOfMilesTest(params object[] yourParamsHere)
        //{
        //    //arrange
        //    throw new NotImplementedException();
        //    //act

        //    //assert
        //}
    }
}