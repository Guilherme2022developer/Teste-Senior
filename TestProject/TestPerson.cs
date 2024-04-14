using Sênior.Business.Models;

namespace TestProject
{
    public class TestPerson
    {
        [SetUp]
        public void Add_Person()
        {
            // Arrange
            var person = new PersonCreate
            {
                Name = "Teste",
                CPF = "12345678901",
                BirthDate = DateTime.Now,
                UF = "SP",
                Email = "gui.alves@gmail.com",
                Telephone = "11999999999",
                LastName = "Teste",
                Active = true,
            };

            // Act

        }
    }
           
 }
