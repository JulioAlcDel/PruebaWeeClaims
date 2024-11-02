using Core.Domain.Entities;
using Core.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;


namespace Core.Application.UseCases.CreateRegister
{
    public class CreateRegisterHandler
    {
        private readonly IRegisterRepository _repository;
        private readonly CreateRegisterValidator _validator;
        private readonly ILogger<CreateRegisterHandler> _logger;
        public CreateRegisterHandler(IRegisterRepository repository,ILogger<CreateRegisterHandler> logger)
        {
            _repository = repository;
            _validator = new CreateRegisterValidator();
            _logger = logger;
        }
        public async Task<int> Handle(CreateRegisterCommand command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("El comando tiene datos no validos");
                throw new ValidationException(validationResult.Errors);
            }
            try
            {
                var contact = new Register()
                {
                    CompanyName = command.CompanyName,
                    Email = command.Email,
                    Contact = command.Contact,
                    Phone = command.Phone,
                    CheckConditions = command.CheckConditions,
                };
                var contactId = await _repository.AddRegister(contact);
                _logger.LogInformation("El registro contacto creado correctamente.");

                return contactId;

            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el contacto");
                throw;
            }


        }
    }
}
