using System.ComponentModel.DataAnnotations;

namespace BlazorOnlineForm.Shared.Validators
{
    public class IdNumberValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string idString = (string)value;
                bool isIdNumber = int.TryParse(idString, out int id);
                if (isIdNumber)
                {
                    if (idString.Length != 9)
                    {
                        var name = validationContext.DisplayName;
                        var specificErrorMessage = ErrorMessage;
                        if (string.IsNullOrEmpty(specificErrorMessage))
                            specificErrorMessage = $"{name} is required.";

                        return new ValidationResult(specificErrorMessage, new[] { validationContext.MemberName });
                    }
                }
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(nameof(IdNumberValidation)));
        }
    }

}
