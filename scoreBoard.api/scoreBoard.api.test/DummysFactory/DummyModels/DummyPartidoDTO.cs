using scoreBoard.api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace scoreBoard.api.test.DummysFactory.DummyModels
{
    public class DummyPartidoDTO
    {   
        public static PartidoDTO Crear()
        {
            return new PartidoDTO
            {
                EquipoLocal = new EquipoDTO("Brasil",0,"BR"),                
                EquipoVisitante = new EquipoDTO("Argentina",0,"ARG")                
            };
        }

        /// <summary>
        /// Mexico - Canada: 0 - 5
        /// Spain - Brazil: 10 – 2
        /// Germany - France: 2 – 2
        /// Uruguay - Italy: 6 – 6
        /// Argentina - Australia: 3 - 1
        /// </summary>
        /// <returns></returns>
        public static List<PartidoDTO> CrearLista()
        {
            List<PartidoDTO> listPartidos = new List<PartidoDTO>();

            var partido_1 = new PartidoDTO
            {
                EquipoLocal = new EquipoDTO("Mexico",0, "ME"),
                EquipoVisitante = new EquipoDTO("Canada", 5, "CA"),
                FechaEncuentro =DateTime.Now
            };            
            var partido_2 = new PartidoDTO
            {
                EquipoLocal = new EquipoDTO("Spain", 10, "SP"),
                EquipoVisitante = new EquipoDTO("Brasil", 2, "BA"),
                FechaEncuentro = DateTime.Now.AddHours(1)
            };
            var partido_3 = new PartidoDTO
            {
                EquipoLocal = new EquipoDTO("Germany", 2, "GE"),
                EquipoVisitante = new EquipoDTO("France", 2, "FR"),
                FechaEncuentro = DateTime.Now.AddHours(2)
            };
            var partido_4 = new PartidoDTO
            {
                EquipoLocal = new EquipoDTO("Uruguay", 6, "UR"),
                EquipoVisitante = new EquipoDTO("Italy", 6, "IT"),
                FechaEncuentro = DateTime.Now.AddHours(3)
            };
            var partido_5 = new PartidoDTO
            {
                EquipoLocal = new EquipoDTO("Argentina", 3, "ARG"),
                EquipoVisitante = new EquipoDTO("Australia", 3, "AST"),
                FechaEncuentro = DateTime.Now.AddHours(4)
            };

            listPartidos.Add(partido_1);
            listPartidos.Add(partido_2);
            listPartidos.Add(partido_3);
            listPartidos.Add(partido_4);
            listPartidos.Add(partido_5);

            return listPartidos;
        }
    }
}
