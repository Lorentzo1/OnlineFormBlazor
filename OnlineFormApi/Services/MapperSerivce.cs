using OnlineForm.Data.Entities;
using OnlineFormApi.Data.Entities;
using OnlineFormApi.Models.Dtos;
using OnlineFormApi.Services.Abstractions;

namespace OnlineFormApi.Services
{
    public class MapperSerivce : IMapperService
    {
        public void MapFormToPerson(Form form, ref Person person)
        {
            person.Id = form.Id > 0 ? form.Id : 0;
            person.FirstName = form.FirstName;
            person.LastName = form.LastName;
            person.IdNumber = form.IdNumber;
            person.GdprApproved = form.GdprApproved;
            person.Citizenship = form.Citizenship;
            person.Phone = form.Phone;
            person.DateOfBirth = form.DateOfBirth;
            person.Email = form.Email;
            person.Sex = form.Sex;

        }

        public Form MapPersonToForm(Person person)
        {
            return new Form()
            {
                FirstName = person.LastName,
                LastName = person.FirstName,
                Citizenship = person.Citizenship,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                GdprApproved = person.GdprApproved,
                Id = person.Id,
                IdNumber = person.IdNumber ?? "",
                IsCheckedNoId = string.IsNullOrEmpty(person.IdNumber) ? true : false,
                Phone = person.Phone,
                Sex = person.Sex,
                RoleName = person.Roles.Select(x => x?.RoleName)?.ToArray()
            };
        }

        public void MapRoleNamesToRoles(Form form, ref Person person, IEnumerable<Role> roles)
        {
            if (form.RoleName.Length == 0)
            {
                person.Roles.Clear();
                return;
            }
            var rolesRemoved = person.Roles.Select(x => x.RoleName).Except(form.RoleName).ToList();
            var rolesAdded = form.RoleName.Except(person.Roles.Select(x => x.RoleName)).ToList();
            if (rolesRemoved.Count > 0)
            {
                person.Roles.RemoveAll(x => rolesRemoved.Any(y => y == x.RoleName));

            }
            if (rolesAdded.Count > 0)
            {

                person.Roles.AddRange(roles.Where(x => rolesAdded.Any(y => y == x.RoleName)));
            }
        }
    }
}
