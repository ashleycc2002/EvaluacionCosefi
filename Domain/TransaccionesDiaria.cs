using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("TransaccionesDiarias", Schema = "dbo")]
    [MetadataType(typeof(TransaccionesDiariaMetaData))]
    public partial class TransaccionesDiaria
    {
        [Key]
        public int IDSecuencial { get; set; }
        public int IDCliente { get; set; }
        public decimal MontoTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("IDCliente")]
        public virtual CuentasAhorros CuentasAhorros { get; set; }
    }

    public class TransaccionesDiariaMetaData
    {
        [Display(Name = "fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaCreacion { get; set; }

        [Required(ErrorMessage ="Debe seleccional el nombre del cliente")]
        [Display(Name = "Nombre Cliente")]
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Debe seleccional el Tipo Transacción")]
        [Display(Name = "Tipo Transacción")]
        public string TipoTransaccion { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Favor de escribir el monto.")]
        public decimal MontoTransaccion { get; set; }
    }
}
