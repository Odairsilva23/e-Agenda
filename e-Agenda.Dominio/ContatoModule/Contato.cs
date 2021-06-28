using System;


namespace eAgenda.Dominio
{
    public class Contato : EntidadeBase
    {
        public string Nome;
        public string Email;
        public string Telefone;
        public string Empresa;
        public string Cargo;

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Empresa = empresa;
            Cargo = cargo;
        }
        public override string Validar()
        {
            string resultadoValidacao = "";

            bool verificaEmail = Email.Contains("@") && Email.Contains(".com");

            if (verificaEmail == false)
            {
                resultadoValidacao += "Email inválido";
            }

            if (Telefone.Length < 11)
            {
                resultadoValidacao += "Telefone inválido";
            }

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}
