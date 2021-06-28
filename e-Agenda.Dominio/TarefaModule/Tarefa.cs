using System;

namespace eAgenda.Dominio
{
    public class Tarefa : EntidadeBase
    {
        public string Titulo;
        public DateTime DataCriacao;
        public DateTime? DataConclusao;
        public int Percentual;

        public Tarefa(string titulo, DateTime dataCriacao, PrioridadeEnum prioridade) 
        {
            Titulo = titulo;
            Prioridade = new Prioridade(prioridade);
            DataCriacao = dataCriacao;
        }

        public Prioridade Prioridade { get; set; }        

        public StatusEnum Status
        {
            get
            {
                return Percentual == 100 ? StatusEnum.Finalizada : StatusEnum.Pendente;
            }
        }

        public void AtualizarPercentual(int p)
        {
            Percentual = p;

            if (Percentual == 100)
            {
                DataConclusao = DateTime.Now.Date;
            }
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (Percentual < 0 || Percentual > 100)
            {

                resultadoValidacao += "Percentual não pode ser menor que 0 nem maior que 100 !! \n";
            }


            if (DataCriacao > DataConclusao)
            {
                resultadoValidacao += "Data de criação não pode ser maior que data conclusão \n";
            }

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

    }
}