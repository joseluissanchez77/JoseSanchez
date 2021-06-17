using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JoseSanchez.Data
{
    public class CiudadTrx
    {
        [NotMapped]
        public int IDCiudad { get; set; }
        [NotMapped]
        public String NombreCiudad { get; set; }
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }


        public int out_cod { get; set; }
        public String out_msj { get; set; }

    }
}
