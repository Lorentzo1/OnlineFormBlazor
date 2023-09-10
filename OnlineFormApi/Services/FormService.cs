using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineForm.Data.Entities;
using OnlineForm.Models;
using OnlineForm.Services.Abstractions;
using OnlineFormApi.Data.Entities;
using OnlineFormApi.Models.Dtos;
using OnlineFormApi.Services.Abstractions;

namespace OnlineForm.Services
{
    public class FormService : IFormService
    {
        private readonly IPersonRepository personRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IMapperService mapperService;
        private readonly ILogger<FormService> logger;

        public FormService(IPersonRepository personRepository, 
            IRoleRepository roleRepository, 
            IMapperService mapperService,
            ILogger<FormService> logger)
        {
            this.personRepository = personRepository;
            this.roleRepository = roleRepository;
            this.mapperService = mapperService;
            this.logger = logger;
        }

        public bool GenerateJsonOfForm(Form form, int Id)
        {
            try
            {
                if (!Directory.Exists($"{Environment.CurrentDirectory}\\GeneratedData"))
                {
                    Directory.CreateDirectory($"{Environment.CurrentDirectory}\\GeneratedData");
                }

                string specificPathWithId = $"{Environment.CurrentDirectory}\\GeneratedData\\JsonForm" + $"{Id}";
                var existingFiles = new DirectoryInfo($"{Environment.CurrentDirectory}\\GeneratedData").GetFiles();
                bool alreadyExists = existingFiles.Any(x => x.Name == $"JsonForm{Id}");
                if (alreadyExists)
                {
                    File.WriteAllText($"{specificPathWithId}_edited_{DateTime.Now.ToString("HHmmss_ddMMyy")}", JsonConvert.SerializeObject(form));
                    return true;
                }

                File.WriteAllText(specificPathWithId, JsonConvert.SerializeObject(form));

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Generate file failed", ex);
                return false;
            }
        }

        public async Task<bool> SubmitNewForm(Form form)
        {
            try
            {
                Person person = new Person();
                mapperService.MapFormToPerson(form, ref person);

                if (form.RoleName is not null && form.RoleName?.Length > 0)
                {
                    var roles = await roleRepository.GetAllRolesByName(form.RoleName!);
                    person.Roles.AddRange(roles);
                }

                int success = await personRepository.SavePerson(person);
                bool jsonSuccess = GenerateJsonOfForm(form, success);
                return success > 1 ? true : false;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", ex);
                return false;
            }
        }
        public async Task<bool> EditExistingForm(Form form)
        {
            try
            {
                var person = await personRepository.GetPerson(form.Id);
                var roles = await roleRepository.GetAllRoles();
                mapperService.MapFormToPerson(form, ref person);
                mapperService.MapRoleNamesToRoles(form, ref person, roles);               
                int result = await personRepository.EditPerson(person);
                GenerateJsonOfForm(form, person.Id);
                return result > 1 ? true : false;

            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", ex);
                return false;
            }
        }

        //TO DO:
        public byte[] GetPdfBytes()
        {
            string pathToPdf = $"{Environment.CurrentDirectory}\\Resources\\GDPR.pdf";
            var pdfPdfArray = File.ReadAllBytes(pathToPdf);

            return pdfPdfArray;
        }


    }
}
