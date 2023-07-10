using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.GRepository;
using Domain;

namespace BLL.Transacciones
{
    public class CuentasAhorroCommandService : ICuentasAhorroCommandService<CuentasAhorros>
    {
        private readonly IRepository<CuentasAhorros> _repository;

        public CuentasAhorroCommandService(IRepository<CuentasAhorros> repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        public void Add(CuentasAhorros entity)
        {
            try
            {
                _repository.Add(entity);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public void Add(params CuentasAhorros[] items)
        {
            try
            {
                _repository.Add(items);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool DepositMoney(decimal monto, int Id)
        {
            bool result = false;
            decimal AvaibleBalance = 0.0m;
            try
            {
                if (monto > 0)
                {
                    var model = _repository.GetSingle(x => x.IDCliente == Id);

                    AvaibleBalance = model.BalanceDisponible;
                    AvaibleBalance += monto;
                    model.BalanceDisponible = AvaibleBalance;
                    model.TransaccionesDiarias = null;

                    _repository.UpdateOne(model,x=> x.BalanceDisponible);
                    _repository.SaveChanges();

                    result = true;
                }
                else
                    result = false;
               
            }
            catch (Exception ex)
            {
               
                throw ex;
            }

            return result;

        }
    

        public void Remove(CuentasAhorros entity)
        {
            try
            {
                _repository.Remove(entity);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public void Remove(params CuentasAhorros[] items)
        {

            try
            {
                _repository.Remove(items);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(CuentasAhorros entity)
        {
            try
            {
                var cuentaAhorros = _repository.GetAll().FirstOrDefault(x=> x.IDCliente.Equals(entity.IDCliente));

                cuentaAhorros.IDCliente = entity.IDCliente;
                cuentaAhorros.NombreCliente = entity.NombreCliente;
                cuentaAhorros.NumeroCuenta = entity.NumeroCuenta;
                cuentaAhorros.MontoApertura = entity.MontoApertura;
                cuentaAhorros.BalanceDisponible = entity.BalanceDisponible;
                cuentaAhorros.FechaCreacion = entity.FechaCreacion;
                cuentaAhorros.activo = entity.activo;

                _repository.Update(cuentaAhorros);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public void Update(params CuentasAhorros[] items)
        {
            try
            {
                var newcastle = new List<CuentasAhorros>();

                var cuentaAhorros = _repository.GetAll().FirstOrDefault(x => x.IDCliente.Equals(items[0].IDCliente));

                foreach (var itm in items)
                {
                    if (cuentaAhorros == null) continue;
                    cuentaAhorros.IDCliente = itm.IDCliente;
                    cuentaAhorros.NombreCliente = itm.NombreCliente;
                    cuentaAhorros.NumeroCuenta = itm.NumeroCuenta;
                    cuentaAhorros.MontoApertura = itm.MontoApertura;
                    cuentaAhorros.BalanceDisponible = itm.BalanceDisponible;
                    cuentaAhorros.FechaCreacion = itm.FechaCreacion;
                    cuentaAhorros.activo = itm.activo;
                }

                newcastle.Add(cuentaAhorros);

                _repository.Update(newcastle.ToArray());
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public string WithdrawMoney(TransaccionesDiaria entity)
        {
            decimal AvaibleBalance = 0.0m;
            string result = string.Empty;

            try
            {
                var model = _repository.GetSingle(x=> x.IDCliente.Equals(entity.IDCliente));
                if (model != null)
                {
                    if (entity.MontoTransaccion > model.BalanceDisponible)
                        result = "no tiene esa cantidad de dinero";
                    else
                    {
                        model.TransaccionesDiarias = null;
                        AvaibleBalance = model.BalanceDisponible;
                        AvaibleBalance -= entity.MontoTransaccion;
                        model.BalanceDisponible = AvaibleBalance;
                        _repository.Update(model);
                        _repository.SaveChanges();
                        result = "Transaccion exitosa.";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        
    }
}
