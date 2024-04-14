using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sênior.Business.Notifications;

namespace SêniorTeste.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class MainController : ControllerBase
    {

        private readonly INotifier _notifier;

        protected MainController(INotifier notification)
        {
            _notifier = notification;
        }

        protected bool OperacaoValida()
        {
            return !_notifier.HasNotification();
        }


        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {

            if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
            return CustomResponse();

        }

        protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in erros)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

                NotifyError(errorMsg);

            }
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }


    }
}
