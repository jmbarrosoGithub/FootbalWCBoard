using System;
using System.ComponentModel.DataAnnotations;

namespace scoreBoard.api.Models
{
    public class EquipoDTO : IEquatable<EquipoDTO>
    {
        [Required]
        public string Id { get; set; }
        public string Nombre { get; set; }

        public int Goles { get; set; }

        
        public bool Equals(EquipoDTO other)
        {
            bool sresp = false;            
            if (other != null && this.Id.Equals(other.Id))
            {
                sresp= true;
            }

            return sresp;
        }
    }
}
