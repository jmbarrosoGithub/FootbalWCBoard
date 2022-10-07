using System;
using System.ComponentModel.DataAnnotations;

namespace scoreBoard.api.Models
{
    public class PartidoDTO
    {
        public String Id { get; set; }

        [Required]
        public EquipoDTO EquipoLocal { get; set; }
        
        [Required]
        public EquipoDTO EquipoVisitante { get; set; }

        public int TotalGolesPartido
        {            
            get
            {
                int _totalGoles = 0;
                if(this.EquipoLocal != null && this.EquipoVisitante != null)
                {
                    _totalGoles= this.EquipoVisitante.Goles + this.EquipoLocal.Goles; ;
                }
                return _totalGoles;
            }
        }
 
        public DateTime FechaEncuentro { get; set; }
        
        public DateTime FechaUltimaActualizacion { get; set; }

    }
}
