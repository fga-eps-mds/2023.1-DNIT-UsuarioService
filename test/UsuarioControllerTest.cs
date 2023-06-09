﻿using app.Controllers;
using dominio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using service.Interfaces;
using System;
using System.Collections.Generic;
using test.Stub;
using Xunit;

namespace test
{
    public class UsuarioControllerTest
    {
        const int CREATED = 201;
        const int INTERNAL_SERVER_ERROR = 500;

        [Fact]
        public void Logar_QuandoLoginForValidado_DeveRetornarOk()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.Logar(usuarioDTO);

            usuarioServiceMock.Verify(service => service.ValidaLogin(usuarioDTO), Times.Once);
            Assert.IsType<OkResult>(resultado);
        }

        [Fact]
        public void Logar_QuandoCredenciaisForemInvalidas_DeveRetornarUnauthorized()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            usuarioServiceMock.Setup(service => service.ValidaLogin(It.IsAny<UsuarioDTO>())).Throws(new UnauthorizedAccessException());

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.Logar(usuarioDTO);

            usuarioServiceMock.Verify(service => service.ValidaLogin(usuarioDTO), Times.Once);
            Assert.IsType<UnauthorizedResult>(resultado);
        }

        [Fact]
        public void Logar_QuandoUsuarioNaoExistir_DeveRetornarNotFound()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            usuarioServiceMock.Setup(service => service.ValidaLogin(It.IsAny<UsuarioDTO>())).Throws(new KeyNotFoundException());

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.Logar(usuarioDTO);

            usuarioServiceMock.Verify(service => service.ValidaLogin(usuarioDTO), Times.Once);
            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact]
        public void CadastrarUsuarioDnit_QuandoUsuarioForCadastrado_DeveRetornarCreated()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.CadastrarUsuarioDnit(usuarioDTO);

            usuarioServiceMock.Verify(service => service.CadastrarUsuarioDnit(usuarioDTO), Times.Once);
            var objeto = Assert.IsType<ObjectResult>(resultado);

            Assert.Equal(CREATED, objeto.StatusCode);
        }

        [Fact]
        public void CadastrarUsuarioDnit_QuandoUsuarioJaExistir_DeveRetornarConflict()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            var excecao = new Npgsql.PostgresException("", "", "", "23505");

            usuarioServiceMock.Setup(service => service.CadastrarUsuarioDnit(It.IsAny<UsuarioDTO>())).Throws(excecao);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.CadastrarUsuarioDnit(usuarioDTO);

            usuarioServiceMock.Verify(service => service.CadastrarUsuarioDnit(usuarioDTO), Times.Once);
            var objeto = Assert.IsType<ConflictObjectResult>(resultado);
        }

        [Fact]
        public void CadastrarUsuarioDnit_QuandoCadastroFalhar_DeveRetornarErroInterno()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            var excecao = new Npgsql.PostgresException("", "", "", "");

            usuarioServiceMock.Setup(service => service.CadastrarUsuarioDnit(It.IsAny<UsuarioDTO>())).Throws(excecao);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.CadastrarUsuarioDnit(usuarioDTO);

            usuarioServiceMock.Verify(service => service.CadastrarUsuarioDnit(usuarioDTO), Times.Once);
            var objeto = Assert.IsType<ObjectResult>(resultado);

            Assert.Equal(INTERNAL_SERVER_ERROR, objeto.StatusCode);
        }

        [Fact]
        public void CadastrarUsuarioTerceiro_QuandoUsuarioForCadastrado_DeveRetornarCreated()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.CadastrarUsuarioTerceiro(usuarioDTO);

            usuarioServiceMock.Verify(service => service.CadastrarUsuarioTerceiro(usuarioDTO), Times.Once);
            var objeto = Assert.IsType<ObjectResult>(resultado);

            Assert.Equal(CREATED, objeto.StatusCode);
        }

        [Fact]
        public void CadastrarUsuarioTerceiro_QuandoUsuarioJaExistir_DeveRetornarConflict()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            var excecao = new Npgsql.PostgresException("", "", "", "23505");

            usuarioServiceMock.Setup(service => service.CadastrarUsuarioTerceiro(It.IsAny<UsuarioDTO>())).Throws(excecao);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.CadastrarUsuarioTerceiro(usuarioDTO);

            usuarioServiceMock.Verify(service => service.CadastrarUsuarioTerceiro(usuarioDTO), Times.Once);
            var objeto = Assert.IsType<ConflictObjectResult>(resultado);
        }

        [Fact]
        public void CadastrarUsuarioTerceiro_QuandoCadastroFalhar_DeveRetornarErroInterno()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            var excecao = new Npgsql.PostgresException("", "", "", "");

            usuarioServiceMock.Setup(service => service.CadastrarUsuarioTerceiro(It.IsAny<UsuarioDTO>())).Throws(excecao);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.CadastrarUsuarioTerceiro(usuarioDTO);

            usuarioServiceMock.Verify(service => service.CadastrarUsuarioTerceiro(usuarioDTO), Times.Once);
            var objeto = Assert.IsType<ObjectResult>(resultado);

            Assert.Equal(INTERNAL_SERVER_ERROR, objeto.StatusCode);
        }

        [Fact]
        public void RecuperarSenha_QuandoRecuperacaoForValidada_DeveRetornarOk()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.RecuperarSenha(usuarioDTO);

            usuarioServiceMock.Verify(service => service.RecuperarSenha(usuarioDTO), Times.Once);
            Assert.IsType<OkResult>(resultado);
        }

        [Fact]
        public void RecuperarSenha_QuandoUsuarioNaoExistir_DeveRetornarNotFound()
        {
            UsuarioStub usuarioStub = new();
            var usuarioDTO = usuarioStub.RetornarUsuarioDnitDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            usuarioServiceMock.Setup(service => service.RecuperarSenha(It.IsAny<UsuarioDTO>())).Throws(new KeyNotFoundException());

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.RecuperarSenha(usuarioDTO);

            usuarioServiceMock.Verify(service => service.RecuperarSenha(usuarioDTO), Times.Once);
            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact]
        public void RedefinirSenha_QuandoRedefinicaoForConcluida_DeveRetornarOk()
        {
            RedefinicaoSenhaStub redefinicaoSenhaStub = new();
            var redefinicaoSenhaDTO = redefinicaoSenhaStub.ObterRedefinicaoSenhaDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.RedefinirSenha(redefinicaoSenhaDTO);

            usuarioServiceMock.Verify(service => service.TrocaSenha(redefinicaoSenhaDTO), Times.Once);
            Assert.IsType<OkResult>(resultado);
        }

        [Fact]
        public void RedefinirSenha_QuandoUsuarioNaoExistir_DeveRetornarNotFound()
        {
            RedefinicaoSenhaStub redefinicaoSenhaStub = new();
            var redefinicaoSenhaDTO = redefinicaoSenhaStub.ObterRedefinicaoSenhaDTO();

            Mock<IUsuarioService> usuarioServiceMock = new();
            usuarioServiceMock.Setup(service => service.TrocaSenha(It.IsAny<RedefinicaoSenhaDTO>())).Throws(new KeyNotFoundException());

            var controller = new UsuarioController(usuarioServiceMock.Object);

            var resultado = controller.RedefinirSenha(redefinicaoSenhaDTO);

            usuarioServiceMock.Verify(service => service.TrocaSenha(redefinicaoSenhaDTO), Times.Once);
            Assert.IsType<NotFoundResult>(resultado);
        }
    }
}
