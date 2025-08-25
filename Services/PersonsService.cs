using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;
using ServiceContracts.Enums;

namespace Services;

public class PersonsService : IPersonsService
{
    // private field
    private readonly List<Person> _persons;
    private readonly ICountriesService _countriesService;

    // constructor
    public PersonsService(bool initialize = true)
    {
        _persons = new();
        _countriesService = new CountriesService();

        if (initialize)
        {
            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("0F490D8B-C2C4-4152-AF84-111860E627FD"),
                PersonName = "Ara",
                Email = "adampney0@redcross.org",
                DateOfBirth = DateTime.Parse("1996-04-11"),
                Gender = "Male",
                Address = "42 Summit Parkway",
                ReceiveNewsLetters = false,
                CountryID = Guid.Parse("334A5068-A3F6-4E3A-BFCC-6C34A389E9CE")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("377AE0D7-2B43-4D85-B183-46E18CF886D4"),
                PersonName = "Ellen",
                Email = "etorel1@yale.edu",
                DateOfBirth = DateTime.Parse("1992-11-05"),
                Gender = "Female",
                Address = "90147 Southridge Alley",
                ReceiveNewsLetters = true,
                CountryID = Guid.Parse("9201EB38-3D7E-439E-A879-7A5CE598A084")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("27BB1D2C-BDFE-4A8C-B8BB-933A228E6716"),
                PersonName = "Pavia",
                Email = "pbillington2@ow.ly",
                DateOfBirth = DateTime.Parse("1996-10-24"),
                Gender = "Female",
                Address = "8 Comanche Hill",
                ReceiveNewsLetters = false,
                CountryID = Guid.Parse("9201EB38-3D7E-439E-A879-7A5CE598A084")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("C983BAC9-EAED-49CB-94BB-6228726B8DEA"),
                PersonName = "Gale",
                Email = "grostern3@linkedin.com",
                DateOfBirth = DateTime.Parse("2000-06-12"),
                Gender = "Male",
                Address = "6543 Vidon Parkway",
                ReceiveNewsLetters = true,
                CountryID = Guid.Parse("A31F0E7E-E4F6-4594-86B7-B415F6FC2983")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("F083ECB7-6B86-4806-A63B-CA2C9F8A4B48"),
                PersonName = "Lissi",
                Email = "lcoldman4@google.cn",
                DateOfBirth = DateTime.Parse("1996-10-10"),
                Gender = "Female",
                Address = "64 Hauk Drive",
                ReceiveNewsLetters = false,
                CountryID = Guid.Parse("A31F0E7E-E4F6-4594-86B7-B415F6FC2983")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("B8741297-817E-485B-8FF3-0006CA98530A"),
                PersonName = "Katharine",
                Email = "kgoding5@ucoz.com",
                DateOfBirth = DateTime.Parse("1999-07-08"),
                Gender = "Female",
                Address = "04521 Mockingbird Trail",
                ReceiveNewsLetters = true,
                CountryID = Guid.Parse("92142B42-121B-467E-900E-1397E0F1F689")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("38419CE7-A18E-48C7-BC27-A1569F60D568"),
                PersonName = "Carlo",
                Email = "cgoldring6@google.co.uk",
                DateOfBirth = DateTime.Parse("1996-10-15"),
                Gender = "Male",
                Address = "5 Dorton Avenue",
                ReceiveNewsLetters = true,
                CountryID = Guid.Parse("CB898F97-4A05-4487-AD12-5E46EA17D625")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("EE1DC581-6157-4D69-9FC6-3EFA5349172D"),
                PersonName = "Nerti",
                Email = "ncazalet7@irs.gov",
                DateOfBirth = DateTime.Parse("1993-05-21"),
                Gender = "Female",
                Address = "7 Brickson Park Avenue",
                ReceiveNewsLetters = false,
                CountryID = Guid.Parse("CB898F97-4A05-4487-AD12-5E46EA17D625")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("27871780-D2D5-4A8F-87BB-6F82E47A46ED"),
                PersonName = "Cassondra",
                Email = "cbotler8@php.net",
                DateOfBirth = DateTime.Parse("1991-12-05"),
                Gender = "Female",
                Address = "835 Buhler Road",
                ReceiveNewsLetters = false,
                CountryID = Guid.Parse("CB898F97-4A05-4487-AD12-5E46EA17D625")
            });

            _persons.Add(new Person()
            {
                PersonID = Guid.Parse("F51E84A7-C24B-4FB7-B883-3F7A11E6A6C8"),
                PersonName = "Nikola",
                Email = "nwelman9@homestead.com",
                DateOfBirth = DateTime.Parse("1999-09-27"),
                Gender = "Male",
                Address = "96755 Spaight Lane",
                ReceiveNewsLetters = true,
                CountryID = Guid.Parse("CB898F97-4A05-4487-AD12-5E46EA17D625")
            });
        }
    }

    private PersonResponse ConvertPersonToPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
        return personResponse;
    }

    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        // check if PersonAddRequest is not null
        if (personAddRequest == null)
        {
            throw new ArgumentNullException(nameof(personAddRequest));
        }

        // Model validation
        ValidationHelper.ModelValidation(personAddRequest);

        // convert personAddRequest into Person type
        Person person = personAddRequest.ToPerson();

        // generate PersonID
        person.PersonID = Guid.NewGuid();

        // add person object to persons list
        _persons.Add(person);

        // convert the Person object into PersonResponse type

        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
        return _persons.Select(temp => ConvertPersonToPersonResponse(temp)).ToList();
    }

    public PersonResponse? GetPersonByPersonID(Guid? personID)
    {
        if (personID == null)
            return null;

        Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);

        if (person == null)
            return null;

        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
    {
        List<PersonResponse> allPersons = GetAllPersons();
        List<PersonResponse> matchingPersons = allPersons;

        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            return matchingPersons;

        switch (searchBy)
        {
            case nameof(PersonResponse.PersonName):
                matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.PersonName)?temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                break;
            case nameof(PersonResponse.Email):
                matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                break;
            case nameof(PersonResponse.DateOfBirth):
                matchingPersons = allPersons.Where(temp => temp.DateOfBirth != null ? temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                break;
            case nameof(PersonResponse.Gender):
                matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                break;
            case nameof(PersonResponse.CountryID):
                matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                break;
            case nameof(PersonResponse.Address):
                matchingPersons = allPersons.Where(temp => !string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                break;
            default:
                matchingPersons = allPersons;
                break;
        }

        return matchingPersons;
    }

    public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy))
            return allPersons;

        List<PersonResponse> sortedPersons = (sortBy, sortOrder)
        switch
        {
            (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
            (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
            (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),
            (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),
            (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
            (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),
            _ => allPersons
        };

        return sortedPersons;
    }

    public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if (personUpdateRequest == null)
            throw new ArgumentNullException(nameof(Person));

        // validation
        ValidationHelper.ModelValidation(personUpdateRequest);

        // get matching person object to update
        Person matchingPerson = _persons.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID);
        if (matchingPerson == null)
            throw new ArgumentException("Given person id doesn't exist");

        // update all details
        matchingPerson.PersonName = personUpdateRequest.PersonName;
        matchingPerson.Email = personUpdateRequest.Email;
        matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
        matchingPerson.Gender = personUpdateRequest.Gender.ToString();
        matchingPerson.CountryID = personUpdateRequest.CountryID;
        matchingPerson.Address = personUpdateRequest.Address;
        matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

        return ConvertPersonToPersonResponse(matchingPerson);
    }

    public bool DeletePerson(Guid? personID)
    {
        if (personID == null)
            throw new ArgumentNullException(nameof(personID));

        Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
        if (person == null)
            return false;

        _persons.RemoveAll(temp => temp.PersonID == personID);

        return true;
    }
}
