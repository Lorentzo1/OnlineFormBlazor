using OnlineFormApi.Data.Entities;

namespace OnlineFormApi.Services.Abstractions
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetAllRolesNames();

        Task<IEnumerable<Role>> GetAllRolesByName(string[] names);

        Task<IEnumerable<Role>> GetAllRoles();
    }
}
