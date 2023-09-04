using Newtonsoft.Json;
using OnlineForm.Models;
using OnlineFormApi.Services.Abstractions;

namespace OnlineFormApi.Services
{
    public class CountryService : IContryService
    {
        private readonly IEnumerable<string> _countries;

        public CountryService()
        {
            _countries = LoadCountries();
        }
        public async Task<IEnumerable<string>> GetCountries()
        {
            if(_countries is not null)
            {
               return await Task.FromResult(_countries);
            }
            else
            {
               return await Task.Run(LoadCountries);
            }

        }

        private IEnumerable<string> LoadCountries()
        {
            try
            {
                string cointriesData = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/Resources/Countries-ISO.json");
                List<ContryISOModel> listOfIso = JsonConvert.DeserializeObject<List<ContryISOModel>>(cointriesData) ?? new List<ContryISOModel>() { new ContryISOModel { Code = "CZ", Name = "Czechia" } };
                List<string> countriesNameList = listOfIso.Select(x => x.Name).ToList();
                return countriesNameList;
            }
            catch
            {
                return new List<string>() { "No Data" };
            }
        }
    }
}
