using System.Data;
using NHibernate;
using Poker.Transportation.Repository.Base.Interfaces;

namespace Poker.Transportation.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
        }

        public IGenericTransaction CreateTransaction()
        {
            return CreateTransaction(IsolationLevel.ReadCommitted);
        }

        public IGenericTransaction CreateTransaction(IsolationLevel isolationLevel)
        {
            ITransaction transaction = _session.BeginTransaction(isolationLevel);
            return new GenericTransaction(transaction);
        }

        public void Dispose()
        {
            if (_session != null)
            {
                if (_session.IsOpen)
                {
                    _session.Close();
                }

                _session.Dispose();
            }
        }
    }
}