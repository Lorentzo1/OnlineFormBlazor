using OnlineForm.Data.Entities;
using OnlineFormApi.Models.Dtos;

namespace OnlineForm.Services.Abstractions
{
    public interface IFormService
    {
        Task<bool> SubmitNewForm(Form form);

        Task<bool> EditExistingForm(Form form);
        bool GenerateJsonOfForm(Form form, int Id);
        byte[] GetPdfBytes();
    }
}
