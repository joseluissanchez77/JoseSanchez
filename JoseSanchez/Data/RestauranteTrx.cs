using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JoseSanchez.Data
{
    [Keyless]
    public class RestauranteTrx
    {

        [NotMapped]
        public int IDRestaurante { get; set; }

        [NotMapped]
        public String NombreRestaurante { get; set; }
        [NotMapped]
        [Required]
        public int IDCiudad { get; set; }
        [NotMapped]
        public int NumeroAforo { get; set; }
        [NotMapped]
        public String Telefono { get; set; }


        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        //public String NombreCiudad { get; set; }


        public int out_cod { get; set; }
        public String out_msj { get; set; }
    }
}
