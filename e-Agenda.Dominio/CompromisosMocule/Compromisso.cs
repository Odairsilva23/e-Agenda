
using System;


namespace eAgenda.Dominio
{
    public class Compromisso : EntidadeBase
    {
        public string Assunto;
        public string Local;
        public DateTime DataAgenda;
        public DateTime HoraInicio;
        public DateTime HoraFim;
        public string LinkWEB;
        public Contato Contato;

        public Compromisso(string assunto, string local, DateTime dataagenda,
            DateTime horainicio, DateTime horafim, string link,Contato contato)
        {
            Assunto = assunto;
            Local = local;
            DataAgenda = dataagenda;
            HoraInicio = horainicio;
            HoraFim = horafim;
            LinkWEB = link;
            Contato = contato;

        }


        public override string Validar()
        {
            string resultadoValidacao = "";
            if (this.DataAgenda.DayOfWeek.ToString() == "Saturday" || this.DataAgenda.DayOfWeek.ToString() == "Sunday")
                resultadoValidacao += "O compromisso não pode ser agendado em finais de semana\n";

            if (this.HoraInicio == this.HoraFim)
                resultadoValidacao += "O horário de início não pode ser o mesmo que o de término\n";


            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}
