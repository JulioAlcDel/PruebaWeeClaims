using Core.Application.UseCases.CreateRegister;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterController : ControllerBase
    {
        private readonly CreateRegisterHandler _createRegisterHandler;
        public RegisterController(CreateRegisterHandler createRegisterHandler)
        {
            _createRegisterHandler = createRegisterHandler;
        }
        [HttpPost]
        public async Task <IActionResult> CreateRegister(CreateRegisterCommand command)
        {
           if(command == null)
            {
                return BadRequest("Los datos de la base son obligatorios");
            }
            int contactId = await _createRegisterHandler.Handle(command);
            return Ok(new {Persona= command, Id=contactId});
        }
    }
}
