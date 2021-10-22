using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VirtualLibrary.Domain.DTOs.Login;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Notifications;

namespace VirtualLibrary.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        public MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        [NonAction]
        public bool OperacaoValida()
        {
            return !_notifier.HasNotifications();
        }

        [NonAction]
        protected ActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
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
                errors = _notifier.GetNotifications().Select(e => e.Message)
            });
        }

        [NonAction]
        protected ActionResult CustomResponse(ModelStateDictionary modalState)
        {
            if(!modalState.IsValid) NotificarErroModelInvalida(modalState);

            return CustomResponse();
        }

         //Esse método pega os erros da modelState e os passa para o NotificarErro
        [NonAction]
        protected void NotificarErroModelInvalida(ModelStateDictionary modalState)
        {
            var errors = modalState.Values.SelectMany(m => m.Errors);

            foreach (var error in errors)
            {
                NotificarErro(error.ErrorMessage);
            }
        }

        [NonAction]
        //Esse metodo pega os erros da modelState e os junta com os erros da camada de serviço
        protected void NotificarErro(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}