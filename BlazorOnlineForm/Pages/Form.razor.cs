using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text;
using System.Net.Http.Json;
using BlazorOnlineForm.Shared.Dtos;
using BlazorOnlineForm.Shared.Components;

namespace BlazorOnlineForm.Pages
{
    public partial class Form : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected ILogger<Form> Logger { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? Id { get; set; }


        private FormDto _formDto;
        private List<string> _listOfContries;
        private List<string> _listOfRoles = new List<string>();
        private HashSet<string> _rolesSelected = new HashSet<string>();
        private PopUp _popUp = new PopUp();
        private ServerErrorPopUp _serverErrorPopUp = new ServerErrorPopUp();

        private bool _IdInputDisable = false;
        private bool _dateOfBirtDisable = true;
        private bool _hideEditForm = false;
        private bool _hideCreateNewFormButton = true;
        private bool _noIdCheckBoxChecked;
        public Form()
        {
            InitializeFormState();
        }
        protected override async Task OnInitializedAsync()
        {
            await GetCountriesList();
            await GetRolesList();
            await LoadFormById();
            await base.OnInitializedAsync();
        }

        private async Task OnValidSubmit()
        {
            HttpResponseMessage response;

            if (Id is not null)
            {
                response = await Http.PutAsJsonAsync("https://localhost:7115/Form", _formDto);
                if (response.IsSuccessStatusCode)
                {
                    _popUp.Show("Form was successfully edited, do you want to create new?", "Success");
                }
                else
                {
                    await _serverErrorPopUp.ShowPopUp(nameof(OnValidSubmit));
                    return;
                }
            }
            else
            {
                response = await Http.PostAsJsonAsync("https://localhost:7115/Form", _formDto);
                if (response.IsSuccessStatusCode)
                {
                    _popUp.Show("Form was successfully saved", "Success");
                }
                else
                {
                    await _serverErrorPopUp.ShowPopUp(nameof(OnValidSubmit));
                    return;
                }
            }

            HideOrShowEditForm();
            InitializeFormDto();
        }

        private void CalculateDateOfBirthFromIdNumber()
        {
            string IdNumber = _formDto.IdNumber;
            try
            {
                if (string.IsNullOrWhiteSpace(IdNumber) && !_formDto.IsCheckedNoId)
                    return;

                if (IdNumber.Length == 10)
                {
                    string dataToParse = IdNumber.Remove(6);
                    if (dataToParse[2] >= '5')
                    {
                        StringBuilder stringBuilder = new StringBuilder(dataToParse);
                        switch (dataToParse[2])
                        {
                            case '6':
                                stringBuilder[2] = '1';
                                break;
                            case '5':
                                stringBuilder[2] = '0';
                                break;
                        }
                        dataToParse = stringBuilder.ToString();
                    }

                    DateTime.TryParseExact(dataToParse, "yyMMdd", CultureInfo.CurrentCulture,
                       System.Globalization.DateTimeStyles.None, out DateTime dateParsed);
                    _formDto.DateOfBirth = dateParsed;
                    _dateOfBirtDisable = true;
                }
                else
                {
                    _formDto.DateOfBirth = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Parse of date of birth from id number was unsuccesful", ex);
            }
        }

        private void OnClear()
        {
            InitializeFormDto();
        }

        private void OnNoIdNumber(ChangeEventArgs e)
        {
            _formDto.IdNumber = string.Empty;
            bool checkValue = (bool)e.Value!;
            _formDto.IsCheckedNoId = checkValue;
            _IdInputDisable = checkValue ? true : false;
            _dateOfBirtDisable = checkValue ? false : true;

        }
        private async Task GetCountriesList()
        {
            try
            {
                _listOfContries = await Http.GetFromJsonAsync<List<string>>("https://localhost:7115/Country") ?? new List<string>() { "No Data" };
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in getting List of countries", ex);
            }
        }

        private async Task GetRolesList()
        {
            try
            {
                _listOfRoles = await Http.GetFromJsonAsync<List<string>>("https://localhost:7115/Role/Names") ?? new List<string>() { "No Data" };
            }
            catch (Exception ex)
            {

                Logger.LogError("Error in getting GetRolesList", ex);
            }
        }

        private async Task OnClickRoles(string roleIndex)
        {
            await Task.Run(() =>
            {
                if (_rolesSelected.Contains(roleIndex))
                {
                    _rolesSelected.Remove(roleIndex);
                }
                else
                {
                    _rolesSelected.Add(roleIndex);
                }
                _formDto.RoleName = _listOfRoles.Where(x => _rolesSelected.Any(y => y == x)).ToArray();
            });
        }

        private void InitializeFormState()
        {
            InitializeFormDto();
            _listOfContries = new List<string>() { "No Data" };
            _listOfRoles = new List<string>() { "No Roles" };
            _formDto.Citizenship = _listOfContries[0];
        }

        private void InitializeFormDto()
        {
            _formDto = new FormDto();
            _formDto.RoleName = new string[] { };
            _formDto.DateOfBirth = DateTime.Now;
        }

        private void HideOrShowEditForm()
        {
            if (_hideEditForm)
            {
                _hideEditForm = false;
                _hideCreateNewFormButton = true;
                _popUp.Hide();
                NavigationManager.NavigateTo("/form");
            }
            else
            {
                _hideEditForm = true;
                _hideCreateNewFormButton = false;
            }
        }

        private async Task LoadFormById()
        {
            if (Id is not null)
            {
                await Task.Run(async () =>
                {

                    _formDto = await Http.GetFromJsonAsync<FormDto>($"https://localhost:7115/Form/{Id}");
                    _noIdCheckBoxChecked = _formDto.IsCheckedNoId;
                });
            }
        }
    }
}
