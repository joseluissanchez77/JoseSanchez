using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JoseSanchez.Data
{
    public class Restaurante
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDRestaurante { get; set; }
        public String NombreRestaurante { get; set; }
        public int IDCiudad { get; set; }
        public int NumeroAforo { get; set; }
        public String Telefono { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        public String NombreCiudad { get; set; }

    }
}
