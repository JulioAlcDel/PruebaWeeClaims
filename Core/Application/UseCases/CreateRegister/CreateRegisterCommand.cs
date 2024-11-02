using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.UseCases.CreateRegister
{
    public class CreateRegisterCommand
    {
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public bool CheckConditions { get; set; }

    }
}
