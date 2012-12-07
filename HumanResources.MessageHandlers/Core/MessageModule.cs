using HumanResources.Domain.Common;
using HumanResources.Messages.Replies;
using NHibernate;
using NHibernate.Context;
using NServiceBus;

namespace HumanResources.MessageHandlers.Core
{
    public class MessageModule : IMessageModule
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IBus _bus;

        public MessageModule(ISessionFactory sessionFactory, IBus bus)
        {
            _sessionFactory = sessionFactory;
            _bus = bus;
        }

        public void HandleBeginMessage()
        {
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public void HandleEndMessage()
        {
            Cleardown();
        }

        public void HandleError()
        {
            //TODO: work out how to handle errors. Is this the correct place for this?
            //_bus.Return(ReturnCode.Error);
        }

        private void Cleardown()
        {
            DomainEvents.ClearCallbacks(); 
            _sessionFactory.GetCurrentSession().Dispose();
            CurrentSessionContext.Unbind(_sessionFactory);
        }
    }
}
