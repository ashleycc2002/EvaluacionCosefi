using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Transacciones
{
   public interface ICuentasAhorroQueryService<T> where T : class
    {
        IEnumerable<T>  CuentaAhorro { get; }
        T GetEntityById(int id);
        IList<T> GetEntitity();
        IList<T> GetALL();
        IQueryable<T> GetAllList(bool intactive);
        T GetEntityByName(string name);
        decimal GetAvailableBalance(int Id);
    }
}
