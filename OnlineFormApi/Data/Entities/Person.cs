using OnlineForm.Models.Enums;
using OnlineFormApi.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineForm.Data.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? IdNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ContractTypes Sex { get; set; }

        public string Email { get; set; }

        public string Citizenship { get; set; }

        public bool GdprApproved { get; set; }

        public int  Phone { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
