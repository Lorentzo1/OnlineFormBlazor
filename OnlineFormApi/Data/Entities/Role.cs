using OnlineForm.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineFormApi.Data.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public List<Person> Persons { get; set; } = new List<Person>();
    }
}
