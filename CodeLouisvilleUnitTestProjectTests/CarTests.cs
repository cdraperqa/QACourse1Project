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
            Car car = new Car();

            //act

            //assert
            using (new AssertionScope())
            {
                //need to check that car inherits from Vehicle type
                //var res2 = car.GetType().GetTypeInfo();
                car.NumberOfTires.Should().Be(4);
            }
        }

        [Theory]
        [InlineData ("Honda", "Civic", true)]
        [InlineData("Honda", "Camry", false)]
        public async void ModelIsValidForMakeTest(string make, string model, bool expectedResult)
        {
            //arrange
            Car car = new Car();
            car.Make = make;
            car.Model = model;

            //act
            bool actualResult = await car.IsValidModelForMakeAsync();

            //assert
            Assert.Equal(expectedResult, actualResult);            
        }

        [Theory]
        [InlineData("Honda", "Camry", 2020, false)]
        [InlineData("Subaru", "WRX", 2020, true)]
        //ADD IN CATCH FOR EXCEPTION FOR CARS <1995
        public async void ModelMadeInYearTest (string make, string model, int year, bool expectedResult)
        {
            //arrange
            Car car = new Car();
            car.Make = make;
            car.Model = model;

            //act
            bool actualResult = await car.WasModelMadeInYearAsync(year);

            //assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void AddPassengersTest()
        {
            //arrange
            int startingPassengers = 0;
            double startingMilesPerGallon = 0;

            Car car = new Car(10, "Toyota", "Camry", 20);
            startingPassengers = car.NumberOfPassengers;
            startingMilesPerGallon = car.MilesPerGallon;
            int passengerChange = 2;

            //act
            car.AddPassengers(passengerChange);
            int endingPassengers = car.NumberOfPassengers;
            double endingMilesPerGallon = car.MilesPerGallon;

            //assert
            using (new AssertionScope())
            {
                endingPassengers.Should().Be(startingPassengers + passengerChange);
                endingMilesPerGallon.Should().Be(startingMilesPerGallon - (passengerChange * 0.2));
                passengerChange = passengerChange * -1;
                car.AddPassengers(passengerChange);
                endingPassengers = car.NumberOfPassengers;
                endingMilesPerGallon = car.MilesPerGallon;
                endingPassengers.Should().Be(startingPassengers);
                endingMilesPerGallon.Should().Be(startingMilesPerGallon);
            }
        }

        [Theory]
        [InlineData (5, 21, 3, 2, 20.6)]
        [InlineData(5, 21, 5, 0, 21)]
        [InlineData(5, 21, 25, 0, 21)]
        public void RemovePassengersTest(int Passengers, double milesPerGallon, int passengerChange, int expectedPassengers, double expectedMPG)
        {
            //arrange
            Car car = new Car(10, "Toyota", "Camry", 20);
            car.MilesPerGallon = milesPerGallon;
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