using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Notifications;

namespace VirtualLibrary.Domain.Interfaces.Notifications
{
    public interface INotifier
    {
        bool HasNotifications();
        void Handle(Notification notification);
        List<Notification> GetNotifications();
    }
}