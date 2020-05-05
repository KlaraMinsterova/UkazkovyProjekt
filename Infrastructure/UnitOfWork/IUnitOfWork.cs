using System;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();

        /// <summary>
        /// Registers an action, which is executed if and only if commit is successful.
        /// </summary>
        void RegisterAction(Action action);
    }
}
