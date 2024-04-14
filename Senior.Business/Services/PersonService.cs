using Sênior.Business.Interfaces;
using Sênior.Business.Models;
using Sênior.Business.Models.Validations;
using Sênior.Business.Notifications;
using System;

namespace Sênior.Business.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository, INotifier notifier) : base(notifier)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Add(PersonCreate person)
        {
            if (!ExecutarValidacao(new PersonCreateValidation(), person)) return false;

            if (await _personRepository.GetByCPF(person.CPF) != null) { Notify("CPF já cadastrado"); return false; }

           return await _personRepository.Add(person);
        }

        public async Task<bool> Update(Person person)
        {
            if (!ExecutarValidacao(new PersonValidation(), person)) return false;

            if(person.Id == 0 || person.Id == null) { Notify("Id não informado"); return false; }

            if (await _personRepository.GetByCPF(person.CPF) != null) { Notify("CPF já cadastrado"); return false; }
          
            return await _personRepository.Update(person);
        }

        public async Task<bool> Remove(int id)
        {
           return await _personRepository.Remove(id);
        }
        public async Task<bool> login(LoginUser login)
        {
            if (!ExecutarValidacao(new LoginValidation(), login)) return false;

            return await _personRepository.login(login);
        }
        public void Dispose() => _personRepository?.Dispose();

        
    }
}
