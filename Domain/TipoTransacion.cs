using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Domain
{
   public enum TipoTransacion
   {
        [Description("Cuenta Credito Deposito")]
        NC = 1,
        [Description("Cuenta Debito Retirar")]
        ND = 2
   }

}
