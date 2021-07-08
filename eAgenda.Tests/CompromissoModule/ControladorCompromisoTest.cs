using eAgenda.Controladores.CompromissoModule;
using eAgenda.Controladores.ContatoModule;
using eAgenda.Controladores.Shared;
using eAgenda.Dominio.CompromissoModule;
using eAgenda.Dominio.ContatoModule;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace eAgenda.Tests.CompromissoModule
{
      [TestClass]
   public class ControladorCompromissoTest
   {
       ControladorContato controladorContato = null;
       ControladorCompromisso controladorCompromisso = null;

       public ControladorCompromissoTest()
       {
           controladorContato = new ControladorContato();
           controladorCompromisso = new ControladorCompromisso();
           Db.Update("DELETE FROM [TBCOMPROMISSO]");
           Db.Update("DELETE FROM [TBCONTATO]");
       }

       [TestMethod]
       public void DeveInserir_Compromisso()
         {
             //arrange
             var novoContato = new Contato("José Pedro", "jose.pedro@gmail.com", "321654987", "JP Ltda", "Dev");
             var novoCompromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), novoContato);

             //action
             controladorContato.InserirNovo(novoContato);
             controladorCompromisso.InserirNovo(novoCompromisso);

             //assert
             var compromissoEncontrado = controladorCompromisso.SelecionarPorId(novoCompromisso.Id);
             compromissoEncontrado.Should().Be(novoCompromisso);
         }
       [TestMethod]
      public void DeveInserirSemContato_Compromisso()
         {
            //arrange
           
            var novoCompromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), null);

            //action
            
            controladorCompromisso.InserirNovo(novoCompromisso);

            //assert
            var compromissoEncontrado = controladorCompromisso.SelecionarPorId(novoCompromisso.Id);
            compromissoEncontrado.Should().Be(novoCompromisso);
         }
       [TestMethod]
       public void DeveEditar_Compromisso()
       {
             //arrange
             var contato = new Contato("José Pedro", "jose.pedro@gmail.com", "321654987", "JP Ltda", "Dev");
             controladorContato.InserirNovo(contato);
             var novoContato = new Contato("Pedro", "pedro@gmail.com", "321654987", "P Ltda", "Dev");
             controladorContato.InserirNovo(novoContato);
             var compromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);
             controladorCompromisso.InserirNovo(compromisso);
             var novoCompromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), novoContato);

             //action
             controladorCompromisso.Editar(compromisso.Id, novoCompromisso);

             //assert
             var compromissoAtualizado = controladorCompromisso.SelecionarPorId(compromisso.Id);
             compromissoAtualizado.Should().Be(novoCompromisso);
       }
       [TestMethod]
       public void DeveEditarComContatoNullAdicionandoContato_Compromisso()
       {
           //arrange
           var novoContato = new Contato("Pedro", "pedro@gmail.com", "321654987", "P Ltda", "Dev");
           controladorContato.InserirNovo(novoContato);
           var compromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), null);
           controladorCompromisso.InserirNovo(compromisso);
           var novoCompromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), novoContato);
     
           //action
           controladorCompromisso.Editar(compromisso.Id, novoCompromisso);
     
           //assert
           var compromissoAtualizado = controladorCompromisso.SelecionarPorId(compromisso.Id);
           compromissoAtualizado.Should().Be(novoCompromisso);
       }
        [TestMethod]
        public void DeveEditarComContatoAdicionandoContatoNull_Compromisso()
        {
            //arrange
            var novoContato = new Contato("Pedro", "pedro@gmail.com", "321654987", "P Ltda", "Dev");
            controladorContato.InserirNovo(novoContato);
            var compromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), novoContato);
            controladorCompromisso.InserirNovo(compromisso);
            var novoCompromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), null);

            //action
            controladorCompromisso.Editar(compromisso.Id, novoCompromisso);

            //assert
            var compromissoAtualizado = controladorCompromisso.SelecionarPorId(compromisso.Id);
            compromissoAtualizado.Should().Be(novoCompromisso);
        }

        [TestMethod]
        public void DeveExcluir_Compromisso()
            {
                //arrange
                var contato = new Contato("José Pedro", "jose.pedro@gmail.com", "321654987", "JP Ltda", "Dev");
                controladorContato.InserirNovo(contato);
                var compromisso = new Compromisso("Assunto qualquer", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);
                controladorCompromisso.InserirNovo(compromisso);

                //action
                controladorCompromisso.Excluir(compromisso.Id);

                //assert
                var compromissoAtualizado = controladorCompromisso.SelecionarPorId(compromisso.Id);
                compromissoAtualizado.Should().BeNull();
            }
        [TestMethod]
        public void DeveSelecionarTodos_Compromisso()
          {
          //arrange
          var contato = new Contato("José Pedro", "jose.pedro@gmail.com", "321654987", "JP Ltda", "Dev");
          controladorContato.InserirNovo(contato);
          var contato2 = new Contato("Pedro", "pedro@gmail.com", "321654987", "JP Ltda", "Dev");
          controladorContato.InserirNovo(contato2);
          var contato3 = new Contato("Carlos", "Carlos@gmail.com", "321654987", "JP Ltda", "Dev");
          controladorContato.InserirNovo(contato3);
          var compromisso = new Compromisso("Asuntos externos", "NDD", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);
          controladorCompromisso.InserirNovo(compromisso);
          var compromisso1 = new Compromisso("Reunião", "padaria", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(10, 30, 00), new TimeSpan(11, 30, 00), contato2);
          controladorCompromisso.InserirNovo(compromisso1);
          var compromisso2 = new Compromisso("fazer orçamento", "posto gasolina", "Meet.com", new DateTime(2021, 09, 15), new TimeSpan(09, 30, 00), new TimeSpan(10, 20, 00), contato3);
          controladorCompromisso.InserirNovo(compromisso2);
          //action
          var compromissos = controladorCompromisso.SelecionarTodos();
       
          //assert
          compromissos.Should().HaveCount(3);
          compromissos[0].Assunto.Should().Be("Asuntos externos");
          compromissos[1].Assunto.Should().Be("Reunião");
          compromissos[2].Assunto.Should().Be("fazer orçamento");
          }
        [TestMethod]
        public void DeveSelecionarTodosPassados_Compromisso()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail.com", "321654987", "JP Ltda", "Dev");
            controladorContato.InserirNovo(contato);
            var contato2 = new Contato("Pedro", "pedro@gmail.com", "321654987", "JP Ltda", "Dev");
            controladorContato.InserirNovo(contato2);
            var contato3 = new Contato("Carlos", "Carlos@gmail.com", "321654987", "JP Ltda", "Dev");
            controladorContato.InserirNovo(contato3);
            var compromisso = new Compromisso("Asuntos externos", "NDD", "Meet.com", new DateTime(2021, 06, 10), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);
            controladorCompromisso.InserirNovo(compromisso);
            var compromisso1 = new Compromisso("Reunião", "padaria", "Meet.com", new DateTime(2021, 06, 30), new TimeSpan(10, 30, 00), new TimeSpan(11, 30, 00), contato2);
            controladorCompromisso.InserirNovo(compromisso1);
            var compromisso2 = new Compromisso("fazer orçamento", "posto gasolina", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(09, 30, 00), new TimeSpan(10, 20, 00), contato3);
            controladorCompromisso.InserirNovo(compromisso2);
            //action
            var compromissos = controladorCompromisso.SelecionarCompromissosPassados(DateTime.Today);

            //assert
            compromissos.Should().HaveCount(2);
            compromissos[0].Assunto.Should().Be("Asuntos externos");
            compromissos[1].Assunto.Should().Be("Reunião");
            
        }
        [TestMethod]
        public void DeveSelecionarTodosfuturos_Compromisso()
        {
            //arrange
            var contato = new Contato("José Pedro", "jose.pedro@gmail.com", "321654987", "JP Ltda", "Dev");
            controladorContato.InserirNovo(contato);
            var contato2 = new Contato("Pedro", "pedro@gmail.com", "321654987", "JP Ltda", "Dev");
            controladorContato.InserirNovo(contato2);
            var contato3 = new Contato("Carlos", "Carlos@gmail.com", "321654987", "JP Ltda", "Dev");
            controladorContato.InserirNovo(contato3);
            var compromisso = new Compromisso("Asuntos externos", "NDD", "Meet.com", new DateTime(2021, 06, 10), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), contato);
            controladorCompromisso.InserirNovo(compromisso);
            var compromisso1 = new Compromisso("Reunião", "padaria", "Meet.com", new DateTime(2021, 06, 30), new TimeSpan(10, 30, 00), new TimeSpan(11, 30, 00), contato2);
            controladorCompromisso.InserirNovo(compromisso1);
            var compromisso2 = new Compromisso("fazer orçamento", "posto gasolina", "Meet.com", new DateTime(2021, 09, 11), new TimeSpan(09, 30, 00), new TimeSpan(10, 20, 00), contato3);
            controladorCompromisso.InserirNovo(compromisso2);
            //action
            var compromissos = controladorCompromisso.SelecionarCompromissosFuturos(DateTime.Today, new DateTime(2021,09,12));

            //assert
            compromissos.Should().HaveCount(1);
            compromissos[0].Assunto.Should().Be("fazer orçamento");

        }
        [TestMethod]
        public void DeveSelecionarValidarDatasE_HorasIguais_Compromisso()
        {
            var compromisso = new Compromisso("Asuntos externos", "NDD", "Meet.com", new DateTime(2021, 07, 12), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), null);
            controladorCompromisso.InserirNovo(compromisso);
            var compromisso1 = new Compromisso("Reunião", "padaria", "Meet.com", new DateTime(2021, 07, 12), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), null);
            string resultado =  controladorCompromisso.InserirNovo(compromisso1);

            resultado.Should().Be("Nesta Data e Horario Voce Ja tem Compromisso Agendado");
            List<Compromisso> compromissos = controladorCompromisso.SelecionarTodos();
            compromissos.Should().HaveCount(1);
        }
        [TestMethod]
        public void DeveSelecionarValidarDatasE_HorasEntreOs_Compromisso()
        {
            var compromisso = new Compromisso("Asuntos externos", "NDD", "Meet.com", new DateTime(2021, 07, 12), new TimeSpan(16, 30, 00), new TimeSpan(17, 30, 00), null);
            controladorCompromisso.InserirNovo(compromisso);
            var compromisso1 = new Compromisso("Reunião", "padaria", "Meet.com", new DateTime(2021, 07, 12), new TimeSpan(15, 00, 00), new TimeSpan(17, 00, 00), null);
            string resultado = controladorCompromisso.InserirNovo(compromisso1);

            resultado.Should().Be("Nesta Data e Horario Voce Ja tem Compromisso Agendado");
            List<Compromisso> compromissos = controladorCompromisso.SelecionarTodos();
            compromissos.Should().HaveCount(1);
        }

    }
}
