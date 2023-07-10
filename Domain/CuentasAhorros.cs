using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("CuentasAhorros", Schema = "dbo")]
    [MetadataType(typeof(CuentasAhorrosMetaData))]
    public partial class CuentasAhorros
    {
        public CuentasAhorros()
        {
            this.TransaccionesDiarias = new HashSet<TransaccionesDiaria>();
        }

        [Key]
        public int IDCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal MontoApertura { get; set; }
        public decimal BalanceDisponible { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool activo { get; set; }

        public virtual ICollection<TransaccionesDiaria> TransaccionesDiarias { get; set; }

    }

    public class CuentasAhorrosMetaData
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [Display(Name = "Nombre Cliente")]
        [MinLength(15, ErrorMessage = "El Nombre Cliente debe ser minimo de 15 caracteres.")]
        [MaxLength(20, ErrorMessage = "El Nombre Cliente debe ser maximo de 20 caracteres.")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "La Numero de cuenta es requerido.")]
        [Display(Name = "Numero Cuenta")]
        [MinLength(10, ErrorMessage = "El Numero Cuenta debe ser minimo de 10 caracteres.")]
        [MaxLength(15, ErrorMessage = "El Numero Cuenta debe ser maximo de 15 caracteres.")]
        public string NumeroCuenta { get; set; }

       
        [Required(ErrorMessage = "El Monto Apertura es requerido.")]
        [Display(Name = "Monto Apertura")]
        [DataType(DataType.Currency, ErrorMessage = "Favor de escribir el Monto Apertura.")]
        public decimal MontoApertura { get; set; }

        [Required(ErrorMessage = "El Balance Disponible es requerido.")]
        [Display(Name = "Balance Disponible")]
        [DataType(DataType.Currency, ErrorMessage = "Favor de escribir el Balance Disponible.")]
        public decimal BalanceDisponible { get; set; }

        [Display(Name = "fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
    }
}
