namespace Sênior.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;


        public Notifier()
        {
            _notifications = new List<Notification>();
        }
        public List<Notification> GetNotifications()
        {
           return _notifications;
        }

        public void Handle(Notification notificacao)
        {
           _notifications.Add(notificacao);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
