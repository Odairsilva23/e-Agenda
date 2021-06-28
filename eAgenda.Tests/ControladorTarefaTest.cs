using eAgenda.Controladores;
using eAgenda.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace eAgenda.Tests
{
    [TestClass]
    public class ControladorTarefaTest
    {
        //public ControladorTarefaTest()
        //{
        //    LimparTabelas();        
        //}

        //private void LimparTabelas()
        //{
        //    throw new NotImplementedException();
        //}

        [TestMethod]
        public void DeveInserirUmaTarefa()
        {
            //arrange
            Tarefa tarefa = new Tarefa("Preparar aula", DateTime.Now, PrioridadeEnum.Alta);

            ControladorTarefa controlador = new ControladorTarefa();

            //action
            controlador.InserirNovo(tarefa);

            //assert
            Assert.IsTrue(tarefa.Id > 0);

            int id = tarefa.Id;

            Tarefa tarefaEncontrada = controlador.SelecionarPorId(id);
            Assert.AreEqual("Preparar aula", tarefaEncontrada.Titulo);
            Assert.AreEqual("Prioridade Alta", tarefaEncontrada.Prioridade);
        }

        [TestMethod]
        public void DeveAtualizarUmaTarefa()
        {
            //arrange
            Tarefa tarefa1 = new Tarefa("Preparar aula", DateTime.Now, PrioridadeEnum.Alta);
            ControladorTarefa controlador = new ControladorTarefa();
            controlador.InserirNovo(tarefa1);
            int id = tarefa1.Id;

            //action
            Tarefa tarefa2 = new Tarefa("Corrigir Provas", DateTime.Now, PrioridadeEnum.Baixa);
            controlador.Editar(id, tarefa2);

            //assert
            Tarefa tarefaAtualizada = controlador.SelecionarPorId(id);
            Assert.AreEqual("Corrigir Provas", tarefaAtualizada.Titulo);
            Assert.AreEqual("Prioridade Baixa", tarefaAtualizada.Prioridade.ToString());
        }

        [TestMethod]
        public void DeveExcluirUmaTarefa()
        {
            //arrange
            Tarefa tarefa1 = new Tarefa("Preparar aula", DateTime.Now, PrioridadeEnum.Alta);
            ControladorTarefa controlador = new ControladorTarefa();
            controlador.InserirNovo(tarefa1);
            int id = tarefa1.Id;

            //action            
            controlador.Excluir(id);

            //assert
            Tarefa tarefaEncontrada = controlador.SelecionarPorId(id);
            Assert.IsNull(tarefaEncontrada);
        }
    }
}
