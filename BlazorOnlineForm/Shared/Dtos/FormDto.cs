using BlazorOnlineForm.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace BlazorOnlineForm.Shared.Dtos
{
    public class FormDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name has limited maximum length")]
        [MinLength(2, ErrorMessage = "Name has limited minimal length")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Last Name has limited maximum length")]
        [MinLength(2, ErrorMessage = "Last Name has limited minimal length")]
        public string LastName { get; set; }

        [NoIdValidation("IsCheckedNoId", false, ErrorMessage = "Id needs to be filled, unless No Id checkbox is checked")]
        [IdNumberValidation(ErrorMessage = "Id must be 9 digits long")]
        public string IdNumber { get; set; }

        public bool IsCheckedNoId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        //change to enum
        public ContractTypes ContractType { get; set; }

        [Required(ErrorMessage = "Email must by filled")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required (ErrorMessage = "Citizenship must be filled")]
        public string? Citizenship { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Box must be checked")]
        public bool GdprApproved { get; set; }
        public string[]? RoleName { get; set; }
    }
}
