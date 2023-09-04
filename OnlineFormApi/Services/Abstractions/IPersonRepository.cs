using OnlineForm.Data.Entities;

namespace OnlineForm.Services.Abstractions
{
    public interface IPersonRepository
    {
        Task<int> SavePerson(Person form);

       Task<Person> GetPerson(int id);

        Task<int> EditPerson(Person newPerson);
        Task<IEnumerable<Person>> GetAllPersons();

        Task<int> DeletePerson(int id);
    }
}
