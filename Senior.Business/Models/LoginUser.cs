using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sênior.Business.Models
{
    public class LoginUser : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
