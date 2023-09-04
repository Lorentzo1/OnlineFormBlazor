namespace OnlineFormApi.Services.Abstractions
{
    public interface IContryService
    {
        Task<IEnumerable<string>> GetCountries();
    }
}
