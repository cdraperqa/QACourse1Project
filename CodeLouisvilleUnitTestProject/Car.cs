using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Text.Json;
using System.Data.SqlTypes;
using static CodeLouisvilleUnitTestProject.CarRepositoryModel;
using System.Reflection.Metadata.Ecma335;
using System.Dynamic;

namespace CodeLouisvilleUnitTestProject
{
    public class Car : Vehicle
    {

        #region Public Properties
            public int NumberOfPassengers { get; private set; }
            
        #endregion

        #region Private Properties
            private HttpClient client = new HttpClient();
            private string baseUrl = "https://vpic.nhtsa.dot.gov/api/";
        #endregion

        public Car()
            : this(0, "", "", 0)
        { }

        public Car(double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            NumberOfTires = 4;
            GasTankCapacity = gasTankCapacity;
            Make = make;
            Model = model;
            MilesPerGallon = milesPerGallon;
            client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<bool> IsValidModelForMakeAsync()
        {
            bool makeModelFound = false;
            var response = await client.GetAsync("vehicles/GetModelsForMake/" + Make + "?format=json");
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //List<CarRepositoryModel> carInfoList;
            CarRepositoryModel carInfo = JsonSerializer.Deserialize<CarRepositoryModel>(content, options);
            foreach (Result car in carInfo.Results)
            { 
                if (car.Model_Name.ToLower() == Model.ToLower() & car.Make_Name.ToLower() == Make.ToLower())
                {
                    makeModelFound = true;
                    break;
                }
            }
            return makeModelFound;
        }

        public async Task<bool> WasModelMadeInYearAsync(int modelYear)
        {
            bool modelYearFound = false;
            if (modelYear < 1995)
            {
                throw new System.ArgumentException();
            }
            else 
            {
                var response = await client.GetAsync("vehicles/GetModelsForMakeYear/make/" + Make + "/modelyear/" + modelYear + "?format=json");
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<CarRepositoryModel> carInfoList;
                try
                {
                    CarRepositoryModel carInfo = JsonSerializer.Deserialize<CarRepositoryModel>(content, options);
                     foreach (Result car in carInfo.Results)
                    {
                        if (car.Model_Name.ToLower() == Model.ToLower() & car.Make_Name.ToLower() == Make.ToLower())
                        {
                            modelYearFound = true;
                            break;
                        }
                    }
                }
                catch (JsonException e)
                {
                    throw e;
                }
                return modelYearFound;
            }
        }

        public void AddPassengers(int addPassengerCount)
        {
            NumberOfPassengers = NumberOfPassengers += addPassengerCount;
            MilesPerGallon = MilesPerGallon - (addPassengerCount * 0.2);
        }

        public void RemovePassengers(int removePassengerCount)
        {
            if (NumberOfPassengers - removePassengerCount < 0)
            {
                NumberOfPassengers = 0;
            }
            else
            { 
                NumberOfPassengers = NumberOfPassengers -= removePassengerCount;
            }
            MilesPerGallon = MilesPerGallon + (removePassengerCount * 0.2);
        }

    }
}

