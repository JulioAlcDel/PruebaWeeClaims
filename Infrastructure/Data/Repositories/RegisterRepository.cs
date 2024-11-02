using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Dapper;

namespace Infrastructure.Data.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationDBContext _context;
        public RegisterRepository(ApplicationDBContext context) { 
            _context = context;
        }
        public async Task<int> AddRegister(Register register)
        {
            var validationTerm = register.CheckConditions == true ? "1": "0";
            var query = $"INSERT INTO Contactos(NombreCompania,Nombre,Email,Telefono,Codiciones) VALUES(@CompanyName,@Contact,@Email,@Phone,{validationTerm}); SELECT CAST(SCOPE_IDENTITY() as int);";
            var contactId = await _context.Connection.ExecuteAsync(query,register);
            return contactId;
        }
    }
}
