using eAgenda.Controladores;
using eAgenda.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.ConsoleApp.TarefaModule
{
    public class TelaCompromisso : TelaCadastroBasico<Compromisso>, ICadastravel
    {
        public readonly ControladorCompromisso controladorCompromisso;
        private readonly ControladorContato controladorContato;
        private readonly ContatoTela telacontato;

        public TelaCompromisso(ControladorCompromisso controlador, ControladorContato ctrContato,
            ContatoTela contatoTela) : base("Cadastro de Compromissos" ,controlador)
        {
            this.controladorCompromisso = controlador;
            this.controladorContato = ctrContato;
            this.telacontato = contatoTela;
        }

        public override bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.Pesquisando)
                return base.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);

            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela(SubtituloDeVisualizacao());

            VisualizarCompromissosPassados();

            VisualizarCompromissosPendentes();

            return true;
        }

        private bool VisualizarCompromissosPassados()
        {
            bool temRegistros = true;

            var comprPASADOS = controladorCompromisso.SelecionarTodosCompromissosPassados();

            Console.WriteLine("\nCompromissos Anteriores:\n");

            if (comprPASADOS.Count == 0)
            {
                ApresentarMensagem("Nenhum compromisso Anterior cadastrado", TipoMensagem.Atencao);
                temRegistros = false;
            }
            else
                ApresentarTabela(comprPASADOS);

            return temRegistros;
        }

        private void VisualizarCompromissosPendentes()
        {
            var tarefasConcluidas = controladorCompromisso.SelecionarTodosCompromissosPendentes();

            Console.WriteLine("\nCompromissos Futuros:\n");

            if (tarefasConcluidas.Count == 0)
                ApresentarMensagem("Nenhum Compromisso Futuro Cadastrado", TipoMensagem.Atencao);
            else
                ApresentarTabela(tarefasConcluidas);
        }
        public override void ApresentarTabela(List<Compromisso> registros)
        {
            string configuracaoColunasTabela = "{0,-5} | {1,-20} | {2,-10} | {3,-10}| {4,-10}| {5,-10}| {6,-40}| {7,-5}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Assunto", "Local", "Data Do Compromisso", "Hr Inicio", "Hr Fim", "Link web", "Nome do Contato");

            foreach (Compromisso compromisso in registros)
            {
                Console.WriteLine(configuracaoColunasTabela,
                    compromisso.Id, compromisso.Assunto, compromisso.Local, compromisso.DataAgenda, compromisso.HoraInicio,
                    compromisso.HoraFim, compromisso.LinkWEB, compromisso.Contato.Nome);
            }
        }

        public override Compromisso ObterRegistro(TipoAcao tipoAcao)
        {

            Console.Write("Digite o Assunto a ser Tratado: ");
            string assunto = Console.ReadLine();
            Console.WriteLine("Digite o Local : ");
            string local = Console.ReadLine();
            Console.Write("Digite a data do Compromiso: ");
            DateTime dataagenda = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Digite a hora de Inicio: ");
            DateTime horainicio = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Digite a hora de Termino: ");
            DateTime horafim = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Caso Remoto o encontro Armazene aqui o Link WEB : ");
            string link = Console.ReadLine();

            telacontato.VisualizarContatos();
            
            Console.Write("Insira o ID do Contato envolvido no Compromisso (Caso Não faça parte da sua lista de Contatos favor isira o Numero 0(zero): ");
            
            int idcontato = Convert.ToInt32(Console.ReadLine());

            Contato contato = null;

            if (idcontato != 0)
                contato = controladorContato.SelecionarPorId(idcontato);

            return new Compromisso(assunto,local,dataagenda,horainicio,horafim,link, contato);
        }
    }
}
