using NHibernate;

namespace ClientServices.Data.Common
{
    public interface ISessionProvider
    {
        ISession GetCurrent();
        void CloseCurrent();
    }
}
