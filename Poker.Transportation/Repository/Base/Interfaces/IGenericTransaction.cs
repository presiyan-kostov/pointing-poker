using System;

namespace Poker.Transportation.Repository.Base.Interfaces
{
    public interface IGenericTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}