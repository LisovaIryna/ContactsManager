using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    // private field
    private readonly List<Country> _countries;

    // constructor
    public CountriesService(bool initialize = true)
    {
        _countries = new List<Country>();
        if (initialize)
        {
            _countries.AddRange(new List<Country>()
            {
                new Country() { CountryID = Guid.Parse("334A5068-A3F6-4E3A-BFCC-6C34A389E9CE"), CountryName = "USA" },
                new Country() { CountryID = Guid.Parse("9201EB38-3D7E-439E-A879-7A5CE598A084"), CountryName = "Canada" },
                new Country() { CountryID = Guid.Parse("A31F0E7E-E4F6-4594-86B7-B415F6FC2983"), CountryName = "UK" },
                new Country() { CountryID = Guid.Parse("92142B42-121B-467E-900E-1397E0F1F689"), CountryName = "India" },
                new Country() { CountryID = Guid.Parse("CB898F97-4A05-4487-AD12-5E46EA17D625"), CountryName = "Australia" }
            });
        }
    }

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        // Validation: countryAddRequest parameter can't be null
        if (countryAddRequest == null)
            throw new ArgumentNullException(nameof(countryAddRequest));

        // Validation: CountryName can't be null
        if (countryAddRequest.CountryName == null)
            throw new ArgumentException(nameof(countryAddRequest));

        // Validation: CountryName can't be duplicate
        if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
        {
            throw new ArgumentException("Given country name already exists");
        }

        // Convert object from CoutryAddRequest to Country type
        Country country = countryAddRequest.ToCountry();

        // generate CountryID
        country.CountryID = Guid.NewGuid();

        // Add country object into _countries
        _countries.Add(country);

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(country => country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryID(Guid? countryID)
    {
        if (countryID == null)
            return null;

        Country? country_response_from_list = _countries.FirstOrDefault(temp => temp.CountryID == countryID);

        if (country_response_from_list == null)
            return null;

        return country_response_from_list.ToCountryResponse();
    }
}
