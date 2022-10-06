using scoreBoard.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scoreBoard.api.SesionRepository
{
    public static class SessionRepository
    {
        private static List<PartidoDTO> TableroPartidos;
        private static object objLock = new object();

        public static List<PartidoDTO> ScoreBoardRepository
        {
            get
            {
                lock (objLock)
                {
                    if (TableroPartidos == null)
                    {
                        TableroPartidos = new List<PartidoDTO>();
                    }
                    return TableroPartidos;
                }
            }
        }

    }
}
