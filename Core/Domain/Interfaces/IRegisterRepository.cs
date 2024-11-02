using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IRegisterRepository
    {
         Task<int> AddRegister(Register register);
    }
}
