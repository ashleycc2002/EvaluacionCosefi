using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UniOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        TransaccionDbContext Context { get; }

        void Save();
    }
}
