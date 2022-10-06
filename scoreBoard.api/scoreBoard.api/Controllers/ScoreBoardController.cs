using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scoreBoard.api.Models;
using scoreBoard.api.SesionRepository;

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
        [HttpPut]
        [Route("StartGame")]
        public async Task<IActionResult> Put(PartidoDTO partidoDTO)
        {
                        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //Validaciones negocio(partido no exista)
            
            partidoDTO.Id = Guid.NewGuid().ToString();
            partidoDTO.FechaEncuentro = DateTime.Now;
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


        //UpdateScore
        //GetSummary of games by total score

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
        
               
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<PartidoDTO>> Get()
        {
            return SessionRepository.ScoreBoardRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
