using eAgenda.Dominio;
using System;
using System.Collections.Generic;
using System.Data;

namespace eAgenda.Controladores
{
    public class ControladorContato : Controlador<Contato>
    {
        #region queries
        private const string sqlInserirContato =
             @"INSERT INTO [TBCONTATOS]
                (
                    [NOME],       
                    [EMAIL],             
                    [TELEFONE],                    
                    [EMPRESA], 
                    [CARGO]            
                )
            VALUES
                (
                    @NOME,
                    @EMAIL,
                    @TELEFONE,
                    @EMPRESA,
                    @CARGO
                )";

        private const string sqlEditarContato =
            @" UPDATE [TBCONTATOS]
                SET 
                    [NOME] = @NOME, 
                    [EMAIL] = @EMAIL, 
                    [TELEFONE] = @TELEFONE, 
                    [EMPRESA] = @EMPRESA,
                    [CARGO] = @CARGO

                WHERE [ID] = @ID";

        private const string sqlExcluirContato =
            @"DELETE FROM [TBCONTATOS] 
                WHERE [ID] = @ID";

        private const string sqlExisteContato =
           @"SELECT 
                COUNT(*) 
            FROM 
                [TBCONTATOS]
            WHERE 
                [ID] = @ID";
        private const string sqlSelecionarTodosOsContatos =
            @"SELECT 
                [ID],       
                [NOME],       
                [EMAIL],             
                [TELEFONE],                    
                [EMPRESA], 
                [CARGO]  
            FROM
                [TBCONTATOS] C
            ORDER BY 
                C.CARGO";
        private const string sqlSelecionarContatoPorId =
           @"SELECT 
                [ID],       
                [NOME],       
                [EMAIL],             
                [TELEFONE],                    
                [EMPRESA], 
                [CARGO]
             FROM
                [TBCONTATOS]
             WHERE 
                [ID] = @ID";
        #endregion
        public override string Editar(int id, Contato registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarContato, ObtemParametrosContato(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                Db.Delete(sqlExcluirContato, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return Db.Exists(sqlExisteContato, AdicionarParametro("ID", id));
        }

        public override string InserirNovo(Contato registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirContato, ObtemParametrosContato(registro));
            }

            return resultadoValidacao;
        }

        public override List<Contato> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosOsContatos, ConverterEmContato);
        }
        
        public Contato SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarContatoPorId, ConverterEmContato, AdicionarParametro("ID", id));
        }
        private Contato ConverterEmContato(IDataReader reader)
        {
            var nome = Convert.ToString(reader["NOME"]);
            var email = Convert.ToString(reader["EMAIL"]);
            var telefone = Convert.ToString(reader["TELEFONE"]);
            var empresa = Convert.ToString(reader["EMPRESA"]);
            var cargo = Convert.ToString(reader["CARGO"]);

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);

            contato.Id = Convert.ToInt32(reader["ID"]);

            return contato;
        }
        private Dictionary<string, object> ObtemParametrosContato(Contato contato)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", contato.Id);
            parametros.Add("NOME", contato.Nome);
            parametros.Add("EMAIL", contato.Email);
            parametros.Add("TELEFONE", contato.Telefone);
            parametros.Add("EMPRESA", contato.Empresa);
            parametros.Add("CARGO", contato.Cargo);

            return parametros;
        }
        private static Dictionary<string, object> AdicionarParametro(string campo, int valor)
        {
            return new Dictionary<string, object>() { { campo, valor } };
        }
    }
}
