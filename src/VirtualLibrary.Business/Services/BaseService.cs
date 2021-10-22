using FluentValidation;
using FluentValidation.Results;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Models;
using VirtualLibrary.Domain.Notifications;

namespace VirtualLibrary.Business.Services
{
    public class BaseService
    {
        private readonly INotifier _notifier;
        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }
        public void Notificar(string mensagem) 
        {
            _notifier.Handle(new Notification(mensagem));
        }

        public void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        public bool ExecuteValidacao<TV, TE>(TV validator, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var ValidationResult = validator.Validate(entity);

            if(ValidationResult.IsValid) return true;

            Notificar(ValidationResult);
            return false;
        }
    }
}