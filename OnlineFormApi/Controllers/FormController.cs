using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using OnlineForm.Data.Entities;
using OnlineForm.Services.Abstractions;
using OnlineFormApi.Models.Dtos;
using OnlineFormApi.Services.Abstractions;

namespace OnlineFormApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService submitFormService;
        private readonly IPersonRepository personRepository;
        private readonly IMapperService mapperService;

        public FormController(IFormService submitFormService,
            IPersonRepository personRepository,
            IMapperService mapperService)
        {
            this.submitFormService = submitFormService;
            this.personRepository = personRepository;
            this.mapperService = mapperService;
        }

        [HttpPost]
        public async Task<IActionResult> PostFormAsync(Form form)
        {
            bool success = await submitFormService.SubmitNewForm(form);
            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Form>> GetFormAsync([FromRoute] int id)
        {
            var person = await personRepository.GetPerson(id);
            if (person is null)
            {
                NotFound();
            }
            return Ok(mapperService.MapPersonToForm(person));
        }

        [HttpGet]
        [Route("/form/getall")]
        public async Task<ActionResult<IEnumerable<Form>>> GetAlltFormsAsync()
        {
            try
            {
                var persons = await personRepository.GetAllPersons();
                var forms = persons.Select(x => mapperService.MapPersonToForm(x)).ToList();
                if (forms.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(forms);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeFormAsync(Form form)
        {
            bool success = await submitFormService.EditExistingForm(form);
            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormAsync([FromRoute] int id)
        {
            int success = await personRepository.DeletePerson(id);

            if (success > 1)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
