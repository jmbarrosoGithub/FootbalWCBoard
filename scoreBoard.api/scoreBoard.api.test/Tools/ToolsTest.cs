using Microsoft.AspNetCore.Mvc;
using scoreBoard.api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace scoreBoard.api.test.Tools
{
    public class ToolsTest
    {
        public static PartidoDTO ActionResultToPartidoDTO(Task<IActionResult> objResp)
        {
            var okResult = objResp.Result as OkObjectResult;
            var modelResp = okResult.Value as PartidoDTO;

            return modelResp;
        }
        public static List<PartidoDTO> ActionResultToListPartidoDTO(Task<IActionResult> objResp)
        {   
            var okResult = objResp.Result as OkObjectResult;
            var lst = okResult.Value as List<PartidoDTO>;

            return lst;
        }

    }
}
