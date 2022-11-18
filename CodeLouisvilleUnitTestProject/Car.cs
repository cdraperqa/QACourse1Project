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
        private readonly HttpClient _client;

        #region Public Properties
        public int NumberOfPassengers { get; private set; }
        #endregion

        public Car()
            : this(0, "", "", 0)
        { }

        public Car(double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            NumberOfTires = 4;
            _client = new ();
            _client.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/");
            GasTankCapacity = gasTankCapacity;
            Make = make;
            Model = model;
            MilesPerGallon = milesPerGallon;
        }

        public async Task<bool> IsValidModelForMakeAsync()
        {
            var model = this.Model;
            string urlSuffix = $"vehicles/GetModelsForMake/{Make}/?format=json";
            var response = await _client.GetAsync(urlSuffix);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<CarRepositoryModel>(content);
            return data.Results.Any(r => r.Model_Name == Model);
        }

        public async Task<bool> WasModelMadeInYearAsync(int modelYear)
        {
            if (modelYear < 1995) throw new System.ArgumentException("No data is available for years before 1995");
            var model = this.Model;
            string urlSuffix = $"vehicles/GetModelsForMakeYear/make/{Make}/modelyear/{modelYear}?format=json";
            var response = await _client.GetAsync(urlSuffix);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<CarRepositoryModel>(content);
            return data.Results.Any(r => r.Model_Name == Model);
        }

        public void AddPassengers(int addPassengerCount)
        {
            NumberOfPassengers += addPassengerCount;
            MilesPerGallon -= (addPassengerCount * 0.2);
        }

        public void RemovePassengers(int removePassengerCount)
        {
            if (NumberOfPassengers - removePassengerCount < 0)
            {
                MilesPerGallon += (NumberOfPassengers * 0.2);
                NumberOfPassengers = 0;
            }
            else
            { 
                NumberOfPassengers -= removePassengerCount;
                MilesPerGallon += (removePassengerCount * 0.2);
            }
        }

    }
}

