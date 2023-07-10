using BLL.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using WepAppTransaction.Common;

namespace WepAppTransaction.Controllers
{
    public class CuentaAhorrosController : Controller
    {
        private readonly ICuentasAhorroCommandService<CuentasAhorros> _cuentasAhorroCmdService;
        private readonly ICuentasAhorroQueryService<CuentasAhorros> _CuentasAhorroQueryService;

     
        public CuentaAhorrosController(ICuentasAhorroCommandService<CuentasAhorros> cuentasAhorroCmdService, ICuentasAhorroQueryService<CuentasAhorros> cuentasAhorroQueryService)
        {
            _cuentasAhorroCmdService = cuentasAhorroCmdService ?? throw new ArgumentException(nameof(cuentasAhorroCmdService));
            _CuentasAhorroQueryService = cuentasAhorroQueryService ?? throw new ArgumentException(nameof(cuentasAhorroQueryService));
        }
        // GET: CuentaAhorros
        public ActionResult Index()
        {
            var cuentaAhorros = _CuentasAhorroQueryService.GetALL().ToList();
            return View(cuentaAhorros);
        }

        // GET: CuentaAhorros/Details/5
        public ActionResult Details(int id)
        {
            var cuentaAhorros = _CuentasAhorroQueryService.GetEntityById(id);
            return View(cuentaAhorros);
        }

        // GET: CuentaAhorros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentaAhorros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CuentasAhorros entity)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _cuentasAhorroCmdService.Add(entity);
                    return RedirectToAction("Index");
                }

                return View(entity);
            }
            catch
            {
                return View();
            }
        }


        public ActionResult WithdrawMoney()
        {
           //metodo para redireccionar al action del otro controlador transaccionesdiaria
            return RedirectToAction("Create", "TransaccionesDiaria");  
        }
    

        // GET: CuentaAhorros/Create
        public ActionResult DepositMoney( int Id)
        {
            ViewBag.BalanceDisponible = _CuentasAhorroQueryService.GetAvailableBalance(Id);
            var cuentaAhorro = _CuentasAhorroQueryService.GetALL().Where(x=> x.IDCliente.Equals(Id)).OrderBy(x => x.IDCliente).ToList();
            ViewBag.NumeroCuenta = cuentaAhorro.FirstOrDefault().NumeroCuenta;
            ViewBag.IDCliente = cuentaAhorro.FirstOrDefault().IDCliente;
            
            ViewBag.cuentaAhorroList = Tool.ConvertToSelectListItemCollection(cuentaAhorro,
                x => x.NombreCliente, s => s.IDCliente.ToString());

            return View();

        }

        [HttpPost, ActionName("DepositMoney")]
        [ValidateAntiForgeryToken]
        public ActionResult DepositMoneyConfirmed(CuentasAhorros entity)
        {
            try
            {
                var nombreCliente = _CuentasAhorroQueryService.GetALL().FirstOrDefault(x => x.IDCliente.Equals(entity.IDCliente))?.NombreCliente;
                entity.NombreCliente =  nombreCliente ?? "";

    
                    _cuentasAhorroCmdService.DepositMoney(entity.MontoApertura, entity.IDCliente);
                    return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentaAhorros/Edit/5
        public ActionResult Edit(int id)
        {
            var cuentaAhorros = _CuentasAhorroQueryService.GetEntityById(id);
            return View(cuentaAhorros);
        }

        // POST: CuentaAhorros/Edit/5
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(CuentasAhorros entity)
        {
            try
            {   
                 _cuentasAhorroCmdService.Update(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentaAhorros/Delete/5
        public ActionResult Delete(int id)
        {
            var cuentaAhorros = _CuentasAhorroQueryService.GetEntityById(id);
            return View(cuentaAhorros);
        }

        // POST: CuentaAhorros/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var cuentaAhorros = _CuentasAhorroQueryService.GetEntityById(id);
                _cuentasAhorroCmdService.Remove(cuentaAhorros);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
    }
}
