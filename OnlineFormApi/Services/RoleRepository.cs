using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using OnlineForm.Services;
using OnlineFormApi.Data.Entities;
using OnlineFormApi.Services.Abstractions;

namespace OnlineFormApi.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly FormDbContext formDbContext;

        public RoleRepository(FormDbContext formDbContext)
        {
            this.formDbContext = formDbContext;
        }
        public async Task<IEnumerable<string>> GetAllRolesNames()
        {
            try
            {
                return await formDbContext.Roles.Select(x => x.RoleName).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }
        public async Task<IEnumerable<Role>> GetAllRolesByName(string[] names)
        {
            try
            {
                return await formDbContext.Roles.Where(x => names.Any(y => y == x.RoleName)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            try
            {
                return await formDbContext.Roles.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
