using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.GRepository;
using Domain;

namespace BLL.Transacciones
{
    public class TransaccionesDiariaQueryService : ITransaccionesDiariaQueryService<TransaccionesDiaria>
    {

        private readonly IRepository<TransaccionesDiaria> _repository;

        public TransaccionesDiariaQueryService(IRepository<TransaccionesDiaria> repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        public IEnumerable<TransaccionesDiaria> CuentaAhorro { get; }

        public IList<TransaccionesDiaria> GeAll(bool intactive) => _repository.GetAll();

        public IList<TransaccionesDiaria> GetALL()
        {
            try
            {
                return _repository.GetAll(x => x.CuentasAhorros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TransaccionesDiaria> GetAllList(bool intactive)
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

        

        public IList<TransaccionesDiaria> GetEntitity()
        {
            try
            {
                return _repository.GetAll(x => x.CuentasAhorros);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public TransaccionesDiaria GetEntityById(int id)
        {
            try
            {
                return _repository.GetSingle(x => x.IDSecuencial.Equals(id));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public TransaccionesDiaria GetEntityByName(string name)
        {
            try
            {
                 return _repository.GetSingle(x => x.Equals("hola"));
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
