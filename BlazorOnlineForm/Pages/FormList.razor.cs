using BlazorOnlineForm.Shared.Components;
using BlazorOnlineForm.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorOnlineForm.Pages
{
    public partial class FormList
    {
        [Inject]
        protected NavigationManager Navigation {get; set;}

        [Inject]
        protected HttpClient Http { get; set;}

        [Inject]
        protected ILogger<FormList> Logger { get; set; }


        //add db call, and code behind
        List<FormDto> forms = new List<FormDto>();
        private ServerErrorPopUp _serverErrorPopUp = new ServerErrorPopUp();
        private PopUp _popUp = new PopUp();


        protected override async Task OnInitializedAsync()
        {
            forms = await GetForms();
            await base.OnInitializedAsync();
        }

        private async Task<List<FormDto>> GetForms()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<List<FormDto>>("https://localhost:7115/form/getall");
                return response;
            }
            catch (JsonException ex)
            {
                _popUp.Show("This page is empty, if you want to add Forms visit /form page", "No Forms present");
                return new List<FormDto>();
            }
            catch (Exception ex)
            {
                
                await _serverErrorPopUp.ShowPopUp(nameof(GetForms));
                Logger.LogError("Error in getting Forms", ex);
                return new List<FormDto>();
            }

        }
        private void NavigateToFormEdit(int id)
        {
            Navigation.NavigateTo($"form/{id}");
        }

        private async Task DeleteForm(int id)
        {
            try
            {
                var response = await Http.DeleteAsync($"https://localhost:7115/Form/{id}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    await _serverErrorPopUp.ShowPopUp(nameof(DeleteForm));
                }
                forms = await GetForms();
            }
            catch (Exception ex)
            {
                await _serverErrorPopUp.ShowPopUp(nameof(DeleteForm));
                Logger.LogError("Error in Deletion", ex);
            }
        }

    }
}
