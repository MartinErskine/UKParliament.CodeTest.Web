using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models.Person;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "PeopleApi")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PersonModel>> Get(int personId)
        {
            var response = await _personService.GetAsync(personId);

            if (response.IsError)
            {
                return StatusCode((int)response.ErrorCode, response.ErrorDescription);
            }

            return Ok(response.Data);
        }

        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<PersonModel>>> Get(string name)
        {
            var response = await _personService.SearchAsync(name);

            if (response.IsError)
            {
                return StatusCode((int)response.ErrorCode, response.ErrorDescription);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PersonModel>> PostAsync(PersonRequestModel personRequestModel)
        {
            var response = await _personService.PostAsync(personRequestModel);

            if (response.IsError)
            {
                return StatusCode((int)response.ErrorCode, response.ErrorDescription);
            }

            return Ok(response.Data);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PersonModel>> Put(PersonPutModel personPutModel)
        {
            var response = await _personService.PutAsync(personPutModel);

            if (response.IsError)
            {
                return StatusCode((int)response.ErrorCode, response.ErrorDescription);
            }

            return Ok(response.Data);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task Delete()
        {
            //TODO: Delete if not booked to a Room.
            //
        }
    }
}
