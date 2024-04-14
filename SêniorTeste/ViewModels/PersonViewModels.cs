using System.ComponentModel.DataAnnotations;

namespace SêniorTeste.API.ViewModels
{
    public class PersonViewModels
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool? Active { get; set; }
        public string? CPF { get; set; }
        public string? UF { get; set; }
    }
}
