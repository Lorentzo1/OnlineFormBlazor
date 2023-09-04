using OnlineForm.Data.Entities;
using OnlineFormApi.Data.Entities;
using OnlineFormApi.Models.Dtos;

namespace OnlineFormApi.Services.Abstractions
{
    public interface IMapperService
    {
        Form MapPersonToForm(Person person);

        void MapFormToPerson(Form form, ref Person person);

        void MapRoleNamesToRoles(Form form, ref Person person, IEnumerable<Role> roles);
    }
}
