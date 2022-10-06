using scoreBoard.api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace scoreBoard.api.test.DummysFactory.DummyModels
{
    public class DummyPartidoDTO
    {
        

        public static PartidoDTO crear()
        {
            return new PartidoDTO
            {
                EquipoLocal = new EquipoDTO
                {
                    Id = "1",
                    Nombre = "Brasil"
                },
                EquipoVisitante = new EquipoDTO
                {
                    Id = "2",
                    Nombre = "Argentina"
                }
            };
        }
    }
}
