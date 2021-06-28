using eAgenda.Controladores;
using eAgenda.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.ConsoleApp.TarefaModule
{
    public class ContatoTela : TelaCadastroBasico<Contato>, ICadastravel
    {
        public readonly ControladorContato controladorContato;
        public ContatoTela(ControladorContato controladorContato) :
            base("Cadastro de Contatos", controladorContato)
        {
            this.controladorContato = controladorContato;
        }

        public override bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.Pesquisando)
                return base.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);

            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela(SubtituloDeVisualizacao());

            VisualizarContatos();
            return true;
        }
        public bool VisualizarContatos()
        {
            bool temcontato = true;
            var contatosSalvos = controladorContato.SelecionarTodos();

            Console.WriteLine("\nContatos Salvos:\n");

            if (contatosSalvos.Count == 0)
                ApresentarMensagem("Nenhum Contato Salvo Até o Momento", TipoMensagem.Atencao);
            else
                ApresentarTabela(contatosSalvos);
            return temcontato;
        }
        public override void ApresentarTabela(List<Contato> registros)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-20} | {2,-20} | {3,-15} | {4,-20} | {5,-20} ";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Email", "Telefone", "Empresa", "Cargo");

            foreach (Contato contato in registros)
            {
                Console.WriteLine(configuracaoColunasTabela,
                    contato.Id, contato.Nome, contato.Email, contato.Telefone, contato.Empresa, contato.Cargo);
            }
        }

        public override Contato ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o Nome do Seu Novo Contato: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Cadastre o Principal Email do Seu Contato: ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite o Telefone do Seu Novo Contato: ");
            string telefone = Console.ReadLine();
            Console.WriteLine("Digite o Nome da Empresa em Qual seu Novo Contato Trabalha: ");
            string empresa = Console.ReadLine();
            Console.WriteLine("Digite o Cargo o qual ele Ocupa: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, telefone, empresa, cargo);
        }
    }
}
