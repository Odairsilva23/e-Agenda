using eAgenda.Controladores;
using eAgenda.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace eAgenda.Tests
{
    [TestClass]
    public class ContatoTest
    {
        private Contato contato;

    
            private ControladorContato controlador;

        public ContatoTest()
        {
            controlador = new ControladorContato();
            
        }
       

        [TestMethod]
        public void DeveSelecionarTodasEmAberto()
        {
            List<Contato> contatos = controlador.SelecionarTodos();

            Assert.IsTrue(contatos.Count > 0);
        }



    }
}
