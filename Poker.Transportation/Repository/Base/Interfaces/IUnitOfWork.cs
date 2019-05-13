using System;
using System.Data;

namespace Poker.Transportation.Repository.Base.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Creates an ITransaction instance with the ReadCommitted isolation level
        /// </summary>
        /// <returns>Created transaction</returns>
        IGenericTransaction CreateTransaction();

        /// <summary>
        /// Creates an ITransaction instance with the given isolation level
        /// </summary>
        /// <param name="isolationLevel">Level of isolation</param>
        /// <returns>Created transaction</returns>
        IGenericTransaction CreateTransaction(IsolationLevel isolationLevel);
    }
}