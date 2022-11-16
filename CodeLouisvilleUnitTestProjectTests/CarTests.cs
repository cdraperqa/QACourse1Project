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

    }
}