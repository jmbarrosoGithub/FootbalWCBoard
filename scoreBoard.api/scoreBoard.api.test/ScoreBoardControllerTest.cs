using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using scoreBoard.api.Controllers;
using scoreBoard.api.Models;
using scoreBoard.api.SesionRepository;
using scoreBoard.api.test.DummysFactory.DummyModels;
using scoreBoard.api.test.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace scoreBoard.api.test
{
    [TestClass]
    public class ScoreBoardControllerTest
    {
        ScoreBoardController controllerTester;

        [TestInitialize]
        public void Init()
        {
            SessionRepository.ScoreBoardRepository.RemoveAll(x => x.Id != null);
            controllerTester = new ScoreBoardController();
        }
        #region POST Test

        [TestMethod]
        public async Task Post_BadRequest_ModelStateIsValid()
        {
            //Arrange
            controllerTester.ModelState.AddModelError("error", "mal formado");

            //Act
            var objResp = controllerTester.Create(null);

            //Assert            
            Assert.IsInstanceOfType(objResp.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Post_ReturnNewPartidoAsync()
        {
            //Arrange            
            var modelDummy = DummyPartidoDTO.Crear();
                        
            //Act
            var objResp = controllerTester.Create(modelDummy);
            var partidoPut = ToolsTest.ActionResultToPartidoDTO(objResp);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(OkObjectResult));           
            Assert.AreSame(modelDummy.EquipoLocal.Id, partidoPut.EquipoLocal.Id);
        }

        #endregion

        #region DELETE Test

        [TestMethod]
        public async Task Delete_NotFoundIdNullOrEmpty()
        {
            //Arrange

            //Act
            var objResp = controllerTester.Delete(null);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Delete_NotFoundPartidoNonExist()
        {
            //Arrange
            string idPartido = Guid.NewGuid().ToString();
            //Act
            var objResp = controllerTester.Delete(idPartido);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Delete_ReturnOK()
        {
            //Arrange            
            var objResp = this.controllerTester.Create(DummyPartidoDTO.Crear());
            var partidoPut = ToolsTest.ActionResultToPartidoDTO(objResp);

            //Act
            var respDel = controllerTester.Delete(partidoPut.Id);
            var partidoDel = ToolsTest.ActionResultToPartidoDTO(respDel);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(OkObjectResult));
            Assert.AreSame(partidoPut.EquipoLocal.Id, partidoDel.EquipoLocal.Id);
        }

        #endregion

        #region UPDATE Test
        [TestMethod]
        public async Task Put_BadRequest_ModelStateIsValid()
        {
            //Arrange
            controllerTester.ModelState.AddModelError("error", "mal formado");

            //Act
            var objResp = controllerTester.Update(null);

            //Assert            
            Assert.IsInstanceOfType(objResp.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Put_NotFoundPartidoNonExist()
        {
            //Arrange
            var partido = DummyPartidoDTO.Crear();

            //Act
            var objResp = controllerTester.Update(partido);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Put_ReturnOKUpdate()
        {
            //Arrange            
            var objResp = this.controllerTester.Create(DummyPartidoDTO.Crear());
            var partidoPut = ToolsTest.ActionResultToPartidoDTO(objResp);

            partidoPut.EquipoLocal.Goles = +1;


            //Act
            var respUpd = controllerTester.Update(partidoPut);
            var partidoUpd = ToolsTest.ActionResultToPartidoDTO(respUpd);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(OkObjectResult));
            Assert.AreEqual(partidoPut.EquipoLocal.Goles, partidoUpd.EquipoLocal.Goles);
        }

        #endregion

        #region GET Test
        [TestMethod]
        public async Task Get_NoContentSummary()
        {
            controllerTester = new ScoreBoardController();

            //Act
            var objResp = controllerTester.GetSumary();

            //Assert            
            Assert.IsInstanceOfType(objResp.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Get_ReturnOkSummary()
        {
            //Arrange
            List<PartidoDTO> listaPartidosDummy = new List<PartidoDTO>();
            foreach (var partido in DummyPartidoDTO.CrearLista())
            {
                var objResp = this.controllerTester.Create(partido);
                var partidoPut = ToolsTest.ActionResultToPartidoDTO(objResp);
                listaPartidosDummy.Add(partidoPut);
            }

            //Act
            var resp = controllerTester.GetSumary();
            
            //Assert
            Assert.IsInstanceOfType(resp.Result, typeof(OkObjectResult));
            var primarPartido = listaPartidosDummy.OrderByDescending(d => d.TotalGolesPartido).ThenByDescending(d => d.FechaEncuentro).First();

            List<PartidoDTO> lst = ToolsTest.ActionResultToListPartidoDTO(resp);

            Assert.AreEqual(primarPartido, lst.First());
        }

        #endregion
    }
    
}
