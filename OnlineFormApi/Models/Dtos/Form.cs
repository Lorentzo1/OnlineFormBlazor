using OnlineForm.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineFormApi.Models.Dtos
{
    public class Form
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdNumber { get; set; }

        public bool IsCheckedNoId { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ContractTypes Sex { get; set; }

        public string? Email { get; set; }

        public int Phone { get; set; }

        public string? Citizenship { get; set; }

        public bool GdprApproved { get; set; }
        public string[]? RoleName { get; set; }
    }
}
