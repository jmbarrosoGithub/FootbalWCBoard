using Microsoft.AspNetCore.Mvc;
using scoreBoard.api.Models;
using scoreBoard.api.SesionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scoreBoard.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreBoardController : Controller
    {
        //ILogger

        /// <summary>
        /// Start a game. Our data partners will send us data for the games when they start, and these should capture (Initial score is 0 – 0).
        /// a.Home team
        /// b.Away team
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("StartGame")]
        public async Task<IActionResult> Create(PartidoDTO partidoDTO)
        {                        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //Validaciones negocio(partido no exista)
            
            partidoDTO.Id = Guid.NewGuid().ToString();
            partidoDTO.FechaEncuentro = DateTime.Now;
            partidoDTO.FechaUltimaActualizacion = partidoDTO.FechaEncuentro;
            SessionRepository.ScoreBoardRepository.Add(partidoDTO);

            return Ok(partidoDTO);
        }


        /// <summary>
        /// Finish game. It will remove a match from the scoreboard
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [Route("FinishGame")]
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound(id);
            }

            PartidoDTO partidoFinalizado = SessionRepository.ScoreBoardRepository.Where(x => x.Id.Equals(id)).FirstOrDefault();
            
            if(partidoFinalizado == null)
            {
                return NotFound(id);
            }

            SessionRepository.ScoreBoardRepository.Remove(partidoFinalizado);

            return Ok(partidoFinalizado);
        }


        /// <summary>
        /// Update score. Receiving the pair score; home team score and away team score updates a game score.
        /// </summary>
        /// <param name="partidoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateScore")]
        public async Task<IActionResult> Update(PartidoDTO partido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PartidoDTO partidoLocalizado = SessionRepository.ScoreBoardRepository.Where(x => x.Id.Equals(partido.Id)).FirstOrDefault();

            if (partidoLocalizado == null)
            {
                return NotFound(partidoLocalizado);
            }


            //Validaciones negocio(partido no exista)
            SessionRepository.ScoreBoardRepository.Where(x => x.Id.Equals(partido.Id)).FirstOrDefault().EquipoLocal.Goles = partido.EquipoLocal.Goles;
            SessionRepository.ScoreBoardRepository.Where(x => x.Id.Equals(partido.Id)).FirstOrDefault().EquipoVisitante.Goles = partido.EquipoVisitante.Goles;
            SessionRepository.ScoreBoardRepository.Where(x => x.Id.Equals(partido.Id)).FirstOrDefault().FechaUltimaActualizacion = DateTime.Now;
            
            return Ok(partido);
        }


        /// <summary>
        /// Get a summary of games by total score. Those games with the same total score will be returned ordered by the most recently added to our system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Summary")]
        public async Task<IActionResult> GetSumary()
        {
            return Ok(SessionRepository.ScoreBoardRepository);
        }      
    }
}
