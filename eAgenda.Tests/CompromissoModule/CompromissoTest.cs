using eAgenda.Dominio.CompromissoModule;
using eAgenda.Dominio.ContatoModule;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace eAgenda.Tests.CompromissoModule
{
    [TestClass]
    public class CompromissoTest
    {
        [TestMethod]
        public void DeveValidar_Assunto()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("", "NDD", "Meet.com", new DateTime(2021, 09, 11),
                                              new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Assunto é obrigatório");
        }

        [TestMethod]
        public void DeveValidar_Data()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("Home Office", "NDD", "Meet.com", DateTime.MinValue,
                                              new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Data é obrigatório");
        }

        [TestMethod]
        public void DeveValidar_HoraInicio()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("Home Office", "NDD", "Meet.com", new DateTime(2021, 09, 11),
                                              TimeSpan.MinValue, new TimeSpan(17, 30, 00), contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Hora Início é obrigatório");
        }

        [TestMethod]
        public void DeveValidar_HoraTermino()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("Home Office", "NDD", "Meet.com", new DateTime(2021, 09, 11),
                                              new TimeSpan(17, 30, 00), TimeSpan.MinValue, contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Hora Término é obrigatório");
        }

        [TestMethod]
        public void DeveValidar_Assunto_Data()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("", "NDD", "Meet.com", DateTime.MinValue,
                                              new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Assunto é obrigatório"
                + Environment.NewLine
                + "O campo Data é obrigatório");
        }

        [TestMethod]
        public void DeveValidar_Assunto_Data_HoraInicio()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("", "NDD", "Meet.com", DateTime.MinValue,
                                              TimeSpan.MinValue, new TimeSpan(17, 30, 00), contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Assunto é obrigatório"
                + Environment.NewLine
                + "O campo Data é obrigatório"
                + Environment.NewLine
                + "O campo Hora Início é obrigatório");
        }

        [TestMethod]
        public void DeveValidar_Assunto_Data_HoraInicio_HoraTermino()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail", "321654987", "JP Ltda", "Desenvolvedor");
            var compromisso = new Compromisso("", "NDD", "Meet.com", DateTime.MinValue,
                                              TimeSpan.MinValue, TimeSpan.MinValue, contato);

            //action
            var resultadoValidacao = compromisso.Validar();

            //assert
            resultadoValidacao.Should().Be("O campo Assunto é obrigatório"
                + Environment.NewLine
                + "O campo Data é obrigatório"
                + Environment.NewLine
                + "O campo Hora Início é obrigatório"
                + Environment.NewLine
                + "O campo Hora Término é obrigatório");
        }
    }
}
