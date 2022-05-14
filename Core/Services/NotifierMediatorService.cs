using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

namespace Core.Services
{
    public class NotifierMediatorService : INotifierMediatorService
    {
        private readonly IEnumerable<INotifier> _notifiers;

        public NotifierMediatorService(IEnumerable<INotifier> notifiers)
        {
            _notifiers = notifiers;
        }

        public void Notify()
        {
            foreach (var item in _notifiers.Where(x => x.CanRun()))
            {
                item.Notify();
            }
        }
    }

}