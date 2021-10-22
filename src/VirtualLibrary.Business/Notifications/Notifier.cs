using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Notifications;

namespace VirtualLibrary.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notificatios;
        public Notifier()
        {
            _notificatios = new List<Notification>();
        }
        public List<Notification> GetNotifications()
        {
            return _notificatios;
        }

        public void Handle(Notification notification)
        {
            _notificatios.Add(notification);
        }

        public bool HasNotifications()
        {
            return _notificatios.Any();
        }
    }
}