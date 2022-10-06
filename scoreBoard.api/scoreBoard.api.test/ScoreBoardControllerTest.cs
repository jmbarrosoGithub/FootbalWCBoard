using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using scoreBoard.api.Controllers;
using scoreBoard.api.Models;
using scoreBoard.api.SesionRepository;
using scoreBoard.api.test.DummysFactory.DummyModels;
using scoreBoard.api.test.Tools;
using System;
using System.Threading.Tasks;

namespace scoreBoard.api.test
{
    [TestClass]
    public class ScoreBoardControllerTest
    {
        ScoreBoardController controllerTester;

        [TestInitialize]
        public void Init()
        {
            controllerTester = new ScoreBoardController();
        }

        [TestMethod]
        public void Put_BadRequest_ModelStateIsValid()
        {
            //Arrange
            controllerTester.ModelState.AddModelError("error", "mal formado");

            //Act
            var objResp = controllerTester.Put(null);

            //Assert            
            Assert.IsInstanceOfType(objResp.Result, typeof(BadRequestObjectResult));
        }

        //Put_BadRequest_ValidacionException

        [TestMethod]
        public async Task Put_ReturnNewPartidoAsync()
        {
            //Arrange            
            var modelDummy = DummyPartidoDTO.crear();
                        
            //Act
            var objResp = controllerTester.Put(modelDummy);
            
            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(OkObjectResult));
            var okResult = objResp.Result as OkObjectResult;
            var actualConfiguration = okResult.Value as PartidoDTO;
            Assert.AreSame(modelDummy.EquipoLocal.Id, ((objResp.Result as OkObjectResult).Value as PartidoDTO).EquipoLocal.Id);
        }

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
            
            //Act
            var objResp = controllerTester.Delete(Guid.NewGuid().ToString());

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Delete_ReturnOK()
        {
            //Arrange            
            var objResp = this.controllerTester.Put(DummyPartidoDTO.crear());
            var partidoPut = ToolsTest.ActionResultToPartidoDTO(objResp);

            //Act
            var respDel = controllerTester.Delete(partidoPut.Id);
            var partidoDel = ToolsTest.ActionResultToPartidoDTO(respDel);

            //Assert
            Assert.IsInstanceOfType(objResp.Result, typeof(OkObjectResult));
            Assert.AreSame(partidoPut.EquipoLocal.Id, partidoDel.EquipoLocal.Id);

        }
    }
    
}
