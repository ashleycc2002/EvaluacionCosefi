using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Transacciones
{
    public interface ICuentasAhorroCommandService<T> where T : class
    {
        void Add(T entity);
        void Add(params T[] items);
        void Remove(T entity);
        void Remove(params T[] items);
        void Update(T entity);
        void Update(params T[] items);
        bool DepositMoney(decimal monto, int Id);
        string WithdrawMoney(TransaccionesDiaria entity);
    }
}
