using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.GRepository;
using Domain;

namespace BLL.Transacciones
{
    public class TransaccionesDiariaCommandService : ITransaccionesDiariaCommandService<TransaccionesDiaria>
    {
        private readonly IRepository<TransaccionesDiaria> _repository;
         

        public TransaccionesDiariaCommandService(IRepository<TransaccionesDiaria> repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        public void Add(TransaccionesDiaria entity)
        {
            try
            {
                entity.activo = true;
                _repository.Add(entity);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public void Add(params TransaccionesDiaria[] items)
        {
            try
            {
                items[0].activo = true;
                _repository.Add(items);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void Remove(TransaccionesDiaria entity)
        {
            try
            {
                _repository.Remove(entity);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public void Remove(params TransaccionesDiaria[] items)
        {
            try
            {
                _repository.Remove(items);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Update(TransaccionesDiaria entity)
        {
            try
            {
                var transaccionesDiaria = _repository.GetAll(x=> x.CuentasAhorros).FirstOrDefault(x =>  x.IDSecuencial.Equals(entity.IDSecuencial));

                transaccionesDiaria.IDSecuencial = entity.IDSecuencial;
                transaccionesDiaria.IDCliente = entity.IDCliente;
                transaccionesDiaria.MontoTransaccion = entity.MontoTransaccion;
                transaccionesDiaria.TipoTransaccion =  ((byte.Parse(entity.TipoTransaccion) == (byte)Domain.TipoTransacion.NC)) ? Enum.GetName(typeof(TipoTransacion), 1) : Enum.GetName(typeof(TipoTransacion), 2);
                transaccionesDiaria.fechaCreacion = entity.fechaCreacion;
                transaccionesDiaria.activo = entity.activo;

                _repository.Update(transaccionesDiaria);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(params TransaccionesDiaria[] items)
        {
            try
            {
                var transaccionesDiaria = new List<TransaccionesDiaria>();

                var ModeltransaccionesDiaria = _repository.GetAll(x=> x.CuentasAhorros).FirstOrDefault(x => x.IDCliente.Equals(items[0].IDCliente));

                foreach (var item in items)
                {
                    if (ModeltransaccionesDiaria == null) continue;
                    ModeltransaccionesDiaria.IDSecuencial = item.IDCliente;
                    ModeltransaccionesDiaria.IDCliente = item.CuentasAhorros.IDCliente;
                    ModeltransaccionesDiaria.MontoTransaccion = item.MontoTransaccion;
                    ModeltransaccionesDiaria.TipoTransaccion = item.TipoTransaccion;
                    ModeltransaccionesDiaria.fechaCreacion = item.fechaCreacion;
                    ModeltransaccionesDiaria.activo = item.activo;
                }

                transaccionesDiaria.Add(ModeltransaccionesDiaria);

                _repository.Update(transaccionesDiaria.ToArray());
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
    }
}
