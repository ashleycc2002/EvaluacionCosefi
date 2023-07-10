using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Transacciones;
using Domain;
using WepAppTransaction.Common;

namespace WepAppTransaction.Controllers
{
    public class TransaccionesDiariaController : Controller
    {
        private readonly ITransaccionesDiariaCommandService<TransaccionesDiaria> _TransaccionesDiariaCmdService;
        private readonly ITransaccionesDiariaQueryService<TransaccionesDiaria> _TransaccionesDiariaQueryService;
        private readonly ICuentasAhorroQueryService<CuentasAhorros> _CuentasAhorroQueryService;
        private readonly ICuentasAhorroCommandService<CuentasAhorros> _CuentasAhorroCommandService;

        public TransaccionesDiariaController(ITransaccionesDiariaCommandService<TransaccionesDiaria> TransaccionesDiariaCmdService, 
                                             ITransaccionesDiariaQueryService<TransaccionesDiaria> TransaccionesDiariaQueryService,
                                             ICuentasAhorroQueryService<CuentasAhorros> cuentasAhorroQueryService,
                                             ICuentasAhorroCommandService<CuentasAhorros> cuentasAhorroCommandService)
        {
            _TransaccionesDiariaCmdService = TransaccionesDiariaCmdService ?? throw new ArgumentException(nameof(TransaccionesDiariaCmdService));
            _TransaccionesDiariaQueryService = TransaccionesDiariaQueryService ?? throw new ArgumentException(nameof(TransaccionesDiariaQueryService));
            _CuentasAhorroQueryService = cuentasAhorroQueryService ?? throw new ArgumentException(nameof(cuentasAhorroQueryService));
            _CuentasAhorroCommandService = cuentasAhorroCommandService ?? throw new ArgumentException(nameof(cuentasAhorroCommandService));
        }
       
        public ActionResult Index()
        {
            var transaccionesDiaria = _TransaccionesDiariaQueryService.GetALL().ToList();
            return View(transaccionesDiaria);
        }

       
        public ActionResult Details(int id)
        {

            var transaccionesDiaria = _TransaccionesDiariaQueryService.GetEntityById(id);
            return View(transaccionesDiaria);
        }

       
        public ActionResult Create()
        {
            var cuentaAhorro = _CuentasAhorroQueryService.GetALL().OrderBy(x=> x.IDCliente).ToList();

            ViewBag.TipotrasacctionEnum = Tool.ToListSelectListItemEnum<TipoTransacion>();
            ViewBag.cuentaAhorroList = Tool.ConvertToSelectListItemCollection(cuentaAhorro,
                x=> x.NombreCliente,s=> s.IDCliente.ToString());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransaccionesDiaria entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var message = _CuentasAhorroCommandService.WithdrawMoney(entity);
                    string TypeTransaction = ((byte.Parse(entity.TipoTransaccion) == (byte)Domain.TipoTransacion.NC)) ?  Enum.GetName(typeof(TipoTransacion), 1) : Enum.GetName(typeof(TipoTransacion), 2);

                    entity.TipoTransaccion = TypeTransaction;
                    _TransaccionesDiariaCmdService.Add(entity);
                    return RedirectToAction("Index", "CuentaAhorros");
                }

                return View(entity);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var cuentaAhorro = _CuentasAhorroQueryService.GetALL().OrderBy(x => x.IDCliente).ToList();

            ViewBag.TipotrasacctionEnum = Tool.ToListSelectListItemEnum<TipoTransacion>();
            ViewBag.cuentaAhorroList = Tool.ConvertToSelectListItemCollection(cuentaAhorro,
                x => x.NombreCliente, s => s.IDCliente.ToString());

            var transaccionesDiaria = _TransaccionesDiariaQueryService.GetALL().FirstOrDefault(x=> x.IDSecuencial == id);
            return View(transaccionesDiaria);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(TransaccionesDiaria entity)
        {
            try
            {
                _TransaccionesDiariaCmdService.Update(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var transaccionesDiaria = _TransaccionesDiariaQueryService.GetEntityById(id);
            return View(transaccionesDiaria);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var cuentaAhorros = _TransaccionesDiariaQueryService.GetEntityById(id);
                _TransaccionesDiariaCmdService.Remove(cuentaAhorros);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
