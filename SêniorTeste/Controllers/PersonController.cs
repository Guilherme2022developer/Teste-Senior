using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sênior.Business.Interfaces;
using Sênior.Business.Models;
using Sênior.Business.Notifications;
using Sênior.Business.Services;
using SêniorTeste.API.Config;
using SêniorTeste.API.ViewModels;
using System.Text.RegularExpressions;

namespace SêniorTeste.API.Controllers
{
   
    [ApiController]
    [Route("web/api/")]
    public class PersonController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly IPersonRepository _personRepository;

        public PersonController(INotifier notifier, 
            IMapper mapper, 
            IPersonService personService, 
            IPersonRepository personRepository) : base(notifier)
        {
            _mapper = mapper;
            _personService = personService;
            _personRepository = personRepository;
        }


        /// <summary>
        /// Login
        ///</summary>
        ///<param name="login"></param>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel login)
        {
            if (login == null) return CustomResponse(StatusCode(StatusCodes.Status404NotFound));

            var success = await _personService.login(_mapper.Map<LoginUser>(login));

            if (success)
            {
                var token = new Token();
                var jwtToken = token.GenerateJwtToken("1", "Pessoa", DateTime.UtcNow.AddHours(1));
                return CustomResponse(new { token = jwtToken });
            }

            return CustomResponse("Usuário ou senha inválidos");
        }

        /// <summary>
        /// Get person by CPF
        /// </summary>
        /// <param name="cpf"></param>
        [AllowAnonymous]
        [HttpGet("person/{cpf}/get")]
        public async Task<ActionResult<PersonViewModels>> GetByCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf))
                {
                    NotifyError("CPF não informado");
                    return CustomResponse();
                }

                var cpfRegex = new Regex(@"^\d{11}$");
                if (!cpfRegex.IsMatch(cpf))
                {
                    NotifyError("CPF inválido");
                    return CustomResponse();
                }

                return CustomResponse(_mapper.Map<PersonViewModels>(await _personRepository.GetByCPF(cpf)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all person by UF
        /// </summary>
        /// <param name="uf"></param>
        [AllowAnonymous]
        [HttpGet("person/{uf}/list")]
        public async Task<ActionResult<List<PersonViewModels>>> GetByUf(string uf)
        {
            try 
            { 
                if (string.IsNullOrEmpty(uf)) { NotifyError("UF vazio, forneça um valido"); return CustomResponse(); } 

                if(uf.Length != 2) { NotifyError("UF inválido, forneça um valido"); return CustomResponse(); }

                return CustomResponse(_mapper.Map<List<PersonViewModels>>(await _personRepository.GetByUf(uf)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }

        /// <summary>
        /// get person by id
        /// </summary>
        /// <param name="id"></param>
        [AllowAnonymous]
        [HttpGet("person/{id}")]
        public async Task<ActionResult<PersonViewModels>> GetByCode(int id)
        {
            try
            {
                var result = _mapper.Map<PersonViewModels>(await _personRepository.GetByCode(id));

                if (result == null) return CustomResponse("Pessoa não encontrada");
                return CustomResponse (result);
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// get all person
        /// </summary>
        [Authorize]
        [HttpGet("person/all")]
        public async Task<ActionResult<List<PersonViewModels>>> GetAll()
        {
            try
            {
                return CustomResponse(_mapper.Map<List<PersonViewModels>>(await _personRepository.GetAll()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
          
        }

        /// <summary>
        /// add person
        /// </summary>
        /// <param name="personViewModels"></param>
        [AllowAnonymous]
        [HttpPost("person/add")]
        public async Task<ActionResult<PersonCreateViewModel>> AddPerson(PersonCreateViewModel personViewModels)
        {

            try
            {
                if (personViewModels == null) return CustomResponse(StatusCode(StatusCodes.Status404NotFound));
                var person = _mapper.Map<PersonCreate>(personViewModels);
                await _personService.Add(person);

                var responseViewModel = _mapper.Map<PersonResponseViewModel>(person);
                return CustomResponse(responseViewModel);

            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// update person
        /// </summary>
        /// <param name="personViewModels"></param>
        [AllowAnonymous]
        [HttpPut("person/update")]
        public async Task<ActionResult<PersonViewModels>> UpdatePerson(PersonViewModels personViewModels)
        {
            try 
            {
                if (personViewModels == null) return CustomResponse(StatusCode(StatusCodes.Status404NotFound));

                var person = _mapper.Map<Person>(personViewModels);
                await _personService.Update(person);

                return CustomResponse(_mapper.Map<PersonViewModels>(person));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// remove person
        /// </summary>
        /// <param name="id"></param>
        [AllowAnonymous]
        [HttpDelete("person/remove/{id}")]
        public async Task<ActionResult<PersonViewModels>> RemovePerson(int id)
        {
            try
            {
                var person = _mapper.Map<Person>(await _personRepository.GetByCode(id));
                if (person == null) return CustomResponse("Pessoa não encontrada");

                await _personService.Remove(id);

                return CustomResponse("Removido com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
