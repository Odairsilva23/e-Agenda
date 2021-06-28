using eAgenda.Controladores;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eAgenda.Dominio;


namespace eAgenda.Tests
{
    [TestClass]
    public class ControladorContatoTests
    {
        //public ControladorContatoTests()
        //{
        //    LimparTabelas();
        //}

        //private void LimparTabelas()
        //{
        //    throw new NotImplementedException();
        //}

        [TestMethod]
        public void DeveInserirUmContato()
        {
            //arrange
            Contato contato = new Contato("Joao","Joao@gmail.com", "9985254525", "Monstros S.A", "Gerente");

            ControladorContato controlador = new ControladorContato();

            //action
            controlador.InserirNovo(contato);

            //assert
            Assert.IsTrue(contato.Id > 0);

            int id = contato.Id;

            Contato contatoencontrado = controlador.SelecionarPorId(id);
            Assert.AreEqual("João", contatoencontrado.Nome);
            Assert.AreEqual("Joao@gmail.com", contatoencontrado.Email);
            Assert.AreEqual("9985254525", contatoencontrado.Telefone);
            Assert.AreEqual("Monstros S.A", contatoencontrado.Empresa);
            Assert.AreEqual("Gerente", contatoencontrado.Cargo);

        }

        [TestMethod]
        public void DeveAtualizarUmContato()
        {
            //arrange
            Contato contato1 = new Contato("Joao", "Joao@gmail.com", "9985254525", "Monstros S.A", "Gerente");
            ControladorContato controlador = new ControladorContato();
            controlador.InserirNovo(contato1);
            int id = contato1.Id;

            //action
            Contato contato2 = new Contato("Joao Pedro", "JoaoPedro@gmail.com", "9985254525", "Monstros S.A", "Gerente");
            controlador.Editar(id, contato2);

            //assert
            Contato ContatoAtualizado = controlador.SelecionarPorId(id);
            Assert.AreEqual("João Pedro", ContatoAtualizado.Nome);
            Assert.AreEqual("JoaoPedro@gmail.com", ContatoAtualizado.Email);
            Assert.AreEqual("9985254525", ContatoAtualizado.Telefone);
            Assert.AreEqual("Monstros S.A", ContatoAtualizado.Empresa);
            Assert.AreEqual("Gerente", ContatoAtualizado.Cargo);
        }

        [TestMethod]
        public void DeveExcluirUmContato()
        {
            //arrange
            Contato contato1 = new Contato("Joao", "Joao@gmail.com", "9985254525", "Monstros S.A", "Gerente");
            ControladorContato controlador = new ControladorContato();
            controlador.InserirNovo(contato1);
            int id = contato1.Id;

            //action            
            controlador.Excluir(id);

            //assert
            Contato contatoncontrado = controlador.SelecionarPorId(id);
            Assert.IsNull(contatoncontrado);
        }

    }
}
