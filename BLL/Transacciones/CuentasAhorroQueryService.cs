using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.GRepository;
using Domain;

namespace BLL.Transacciones
{
    public class CuentasAhorroQueryService : ICuentasAhorroQueryService<CuentasAhorros>
    {

        private readonly IRepository<CuentasAhorros> _repository;

        public CuentasAhorroQueryService(IRepository<CuentasAhorros> repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        public IEnumerable<CuentasAhorros> CuentaAhorro { get; }

        public IList<CuentasAhorros> GeAll(bool intactive) => _repository.GetAll();

        public IList<CuentasAhorros> GetALL()
        {
            try
            {
                return _repository.GetAll(x => x.TransaccionesDiarias);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<CuentasAhorros> GetAllList(bool intactive)
        {
            try
            {
                return _repository.GetEntity(intactive);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public decimal GetAvailableBalance(int Id)
        {
            try
            {
                var model = _repository.GetSingle(x => x.IDCliente == Id);
                return model.BalanceDisponible;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<CuentasAhorros> GetEntitity()
        {
            try
            {
                return _repository.GetAll(x=>x.TransaccionesDiarias);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public CuentasAhorros GetEntityById(int id)
        {
            try
            {
                return _repository.GetSingle(x => x.IDCliente.Equals(id));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public CuentasAhorros GetEntityByName(string name)
        {
            try
            {
                return _repository.GetSingle(x => x.NombreCliente.Equals(name));
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
