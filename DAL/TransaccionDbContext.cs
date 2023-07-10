using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public partial class TransaccionDbContext : DbContext
    {
        //public const string conectionString = @"Data Source=DESKTOP-S1KPBO3;Initial Catalog=Evaluacion;Integrated Security=SSPI; User ID=sa;Password=rossy2233;";
        
        public TransaccionDbContext():base("DBCS")
        {
            Database.SetInitializer<TransaccionDbContext>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
         public DbSet<CuentasAhorros> cuentasAhorros { get; set; }
        public DbSet<TransaccionesDiaria> transaccionesDiarias { get; set; }
    }
}
