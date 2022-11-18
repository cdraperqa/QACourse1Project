using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class CarTests
    {
        //Verify that the SemiTruck constructor creates a new SemiTruck
        //object which is also a Vehicle and has 18 wheels. Verify that the
        //Cargo property for the newly created SemiTruck is a List of
        //CargoItems which is empty, but not null.
        [Fact]
        public void NewCarIsAVehicleAndHas4TiresTest()
        {
            //arrange
            Car car = new();

            //act

            //assert
            using (new AssertionScope())
            {
                //need to check that car inherits from Vehicle type
                car.GetType().IsSubclassOf(typeof(Vehicle)).Should().Be(true);
                car.NumberOfTires.Should().Be(4);
            }
        }

        [Theory]
        [InlineData ("Honda", "Civic", true)]
        [InlineData("Honda", "Camry", false)]
        public async void ModelIsValidForMakeTest(string make, string model, bool expectedResult)
        {
            //arrange
            Car car = new()
            {
                Make = make,
                Model = model
            };

            //act
            bool actualResult = await car.IsValidModelForMakeAsync();

            //assert
            actualResult.Should().Be(expectedResult);   
        }

        [Fact]
        public async void ModelMadeInYearTestNegativeTest()
        {
            //arrange
            Exception exception = new();
            Car car = new()
            {
                Make = "Ford",
                Model = "Escort"
            };

            //act
            try
            {
                bool actualResult = await car.WasModelMadeInYearAsync(1990);
            }
            catch (Exception ex)
            { 
                exception = ex;
            }

            //assert
            using (new AssertionScope())
            {
                exception.GetType().Name.Should().Be("ArgumentException");
                exception.Message.Should().Be("No data is available for years before 1995");
            }
        }

        [Theory]
        [InlineData("Honda", "Camry", 2020, false)]
        [InlineData("Subaru", "WRX", 2020, true)]
        [InlineData("Subaru", "WRX", 2000, false)]
        public async void ModelMadeInYearTestPositiveTest (string make, string model, int year, bool expectedResult)
        {
            //arrange
            Car car = new()
            {
                Make = make,
                Model = model
            };

            //act
            bool actualResult = await car.WasModelMadeInYearAsync(year);

            //assert
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void AddPassengersTest()
        {
            //arrange
            int passengerChange = 2;
            Car car = new(10, "Toyota", "Camry", 20);
            int startingPassengers = car.NumberOfPassengers;
            double startingMilesPerGallon = car.MilesPerGallon;

            //act
            car.AddPassengers(passengerChange);

            //assert
            using (new AssertionScope())
            {
                car.NumberOfPassengers.Should().Be(startingPassengers + passengerChange);
                car.MilesPerGallon.Should().Be(startingMilesPerGallon - (passengerChange * 0.2));
                passengerChange *= -1;
                car.AddPassengers(passengerChange);
                car.NumberOfPassengers.Should().Be(startingPassengers);
                car.MilesPerGallon.Should().Be(startingMilesPerGallon);
            }
        }

        [Theory]
        [InlineData (5, 21, 3, 2, 20.6)]
        [InlineData(5, 21, 5, 0, 21)]
        [InlineData(5, 21, 25, 0, 21)]
        public void RemovePassengersTest(int Passengers, double milesPerGallon, int passengerChange, int expectedPassengers, double expectedMPG)
        {
            //arrange
            Car car = new(10, "Toyota", "Camry", 20)
            {
                MilesPerGallon = milesPerGallon
            };
            car.AddPassengers(Passengers);

            //act
            car.RemovePassengers(passengerChange);

            //assert
            using (new AssertionScope())
            {
                car.NumberOfPassengers.Should().Be(expectedPassengers);
                car.MilesPerGallon.Should().Be(expectedMPG);
            }
        }

    }
}