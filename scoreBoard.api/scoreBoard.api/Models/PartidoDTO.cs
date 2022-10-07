﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace scoreBoard.api.Models
{
    public class PartidoDTO
    {
        public String Id { get; set; }

        [Required]
        public EquipoDTO EquipoLocal { get; set; }
        
        [Required]
        public EquipoDTO EquipoVisitante { get; set; }
 
        public DateTime FechaEncuentro { get; set; }
        
        public DateTime FechaUltimaActualizacion { get; set; }

    }
}
