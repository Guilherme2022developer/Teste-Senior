using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sênior.Business.Models
{
    public class PersonCreate : Entity
    {
            public string? Name { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Telephone { get; set; }
            public DateTime BirthDate { get; set; }
            public bool? Active { get; set; }
            public string? CPF { get; set; }
            public string? UF { get; set; }
            public string? Login { get; set; }
            public string? Password { get; set; }
        }
}
