using NHibernate;

namespace Finance.Data.Common
{
    public interface ISessionProvider
    {
        ISession GetCurrent();
        void CloseCurrent();
    }
}
