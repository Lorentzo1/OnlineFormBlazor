using Microsoft.EntityFrameworkCore;
using OnlineForm.Data.Entities;
using OnlineForm.Services.Abstractions;
using OnlineFormApi.Data.Entities;
using System;

namespace OnlineForm.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly FormDbContext dbContext;

        public PersonRepository(FormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> DeletePerson(int id)
        {
            try
            {
                var person = await GetPerson(id);
                dbContext.Remove(person!);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public async Task<int> EditPerson(Person newPerson)
        {
            try
            {
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            try
            {
                return await dbContext.Person
                    .Include(x => x.Roles)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<Person> GetPerson(int id)
        {
            try
            {
                return await dbContext.Person
                    .Where(x => x.Id == id)
                    .Include(x => x.Roles).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SavePerson(Person form)
        {
            try
            {
                await dbContext.AddAsync(form);
                int result = await dbContext.SaveChangesAsync();
                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
