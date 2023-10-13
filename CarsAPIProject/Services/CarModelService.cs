using CarsAPIProject.Entities;
using CarsAPIProject.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarsAPIProject.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly HttpClient _httpClient;

        #region Constructor :: CarModelService
        public CarModelService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        #endregion

        #region Methods
        #region Methods  :: GetModelsForMakeAndYear
        public async Task<string[]> GetModelsForMakeAndYear(string sMake, int nModelYear)
        {
            try
            {
                CarInfo oCarInfo = new CarInfo();
                List<CarInfo> lstCarInfo = ReadDataFromCsvFile();
                if (lstCarInfo != null && lstCarInfo.Count > 0)
                {
                    oCarInfo = lstCarInfo.Where(e => e.make_name.ToLower() == sMake.ToLower()).FirstOrDefault();
                }
                if (oCarInfo != null)
                {
                    string sAPIUrl = $"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{oCarInfo.make_id}/modelyear/{nModelYear}?format=json";
                    HttpResponseMessage response = await _httpClient.GetAsync(sAPIUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        CarModelsResponse OCarModelsResponse = JsonConvert.DeserializeObject<CarModelsResponse>(jsonString);
                        string[] sArrModels;
                        if (OCarModelsResponse != null && OCarModelsResponse.Results.Count > 0)
                        {
                            sArrModels = OCarModelsResponse.Results.Select(e => e.Model_Name).ToArray();
                            return sArrModels;
                        }
                    }
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }
            return new string[0];
        }
        #endregion
        #region Methods :: ReadDataFromCsvFile
        private List<CarInfo> ReadDataFromCsvFile()
        {
            try
            {
                string sBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string sFullFilePath = Path.Combine(sBaseDirectory.Replace("\\bin\\Debug\\net5.0\\", "\\Documents\\CarMake.csv"));
                using (var reader = new StreamReader(sFullFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var vCarInfo = csv.GetRecords<CarInfo>().ToList();
                    return vCarInfo;
                }
            }
            catch (Exception oException)
            {

                throw oException;
            }
        }
        #endregion 
        #endregion
    }
}

