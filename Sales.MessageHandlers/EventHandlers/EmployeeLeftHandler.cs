using HumanResources.Messages.Events;
using NServiceBus;

namespace Sales.MessageHandlers.EventHandlers
{
    public class EmployeeLeftHandler : IHandleMessages<EmployeeLeft>
    {
        public void Handle(EmployeeLeft @event)
        {
            
        }
    }
}
