using System.Collections.Generic;
using NHibernate;
using Poker.Transportation.Repository.Base.Interfaces;

namespace Poker.Transportation.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    {
        protected RepositoryBase(ISession session)
        {
            Session = session;
        }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public void AddOrUpdate(T model)
        {
            bool transactionStarted = StartTransaction(Session);

            Session.SaveOrUpdate(model);

            if (transactionStarted)
            {
                Session.Transaction.Commit();
            }
        }

        protected ISession Session { get; }

        protected static bool StartTransaction(ISession session)
        {
            if (session.Transaction == null || !session.Transaction.IsActive)
            {
                session.BeginTransaction();
                return true;
            }

            return false;
        }
    }
}