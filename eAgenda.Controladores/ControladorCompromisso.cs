using eAgenda.Dominio;
using System;
using System.Collections.Generic;
using System.Data;

namespace eAgenda.Controladores
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {  
        #region queries
        private const string sqlInserirCompromisso =
             @"INSERT INTO [TBCOMPROMISSO]
                (
                    [ASSUNTO],       
                    [LOCAL],             
                    [DATAAGENDADA],                    
                    [HORAINICIO], 
                    [HORAFIM],
                    [LINKWEBCONVERSA], 
                    [ID_CONTATOS]
                )
            VALUES
                (
                    @ASSUNTO,
                    @LOCAL,
                    @DATAAGENDADA,
                    @HORAINICIO,
                    @HORAFIM,
                    @LINKWEBCONVERSA,
                    @ID_CONTATOS
                )";

        private const string sqlEditarCompromisso =
            @" UPDATE [TBCOMPROMISSO]
                SET 
                    [ASSUNTO] = @ASSUNTO, 
                    [LOCAL] = @LOCAL, 
                    [DATAAGENDADA] = @DATAAGENDADA, 
                    [HORAINICIO] = @HORAINICIO,
                    [HORAFIM] = @HORAFIM,
                    [LINKWEBCONVERSA] = @LINKWEBCONVERSA,
                    [ID_CONTATOS] = @ID_CONTATOS

                WHERE [ID] = @ID";

        private const string sqlExcluirCompromisso =
            @"DELETE FROM [TBCOMPROMISSO] 
                WHERE [ID] = @ID";

        private const string sqlExisteCompromisso =
           @"SELECT 
                COUNT(*) 
            FROM 
                [TBCOMPROMISSO]
            WHERE 
                [ID] = @ID";
        private const string sqlSelecionarTodosOsCompromisso =
            @"SELECT 
                C.[ID],
                C.[ASSUNTO],       
                C.[LOCAL],             
                C.[DATAAGENDADA],                    
                C.[HORAINICIO], 
                C.[HORAFIM],
                C.[LINKWEBCONVERSA],
                C.[ID_CONTATOS],
                CO.[NOME], 
                CO.[EMAIL],
                CO.[TELEFONE],
                CO.[EMPRESA],
                CO.[CARGO]
            FROM
                [TBCOMPROMISSO] C LEFT JOIN
                [TBCONTATOS] CO
            ON 
                C.ID_CONTATOS = CO.ID";
        private const string sqlSelecionarCompromissoPorId =
           @"SELECT 
                C.[ID],
                C.[ASSUNTO],       
                C.[LOCAL],             
                C.[DATAAGENDADA],                    
                C.[HORAINICIO], 
                C.[HORAFIM],
                C.[LINKWEBCONVERSA],
                C.[ID_CONTATOS],
                CO.[NOME], 
                CO.[EMAIL],
                CO.[TELEFONE],
                CO.[EMPRESA],
                CO.[CARGO]
            FROM
                [TBCOMPROMISSO] C LEFT JOIN
                [TBCONTATOS] CO
            ON 
                C.ID_CONTATOS = CO.ID
             WHERE 
                C.[ID] = @ID";
        private const string sqlSelecionarCompromissosPassados =
           @"SELECT 
                C.[ID],
                C.[ASSUNTO],       
                C.[LOCAL],             
                C.[DATAAGENDADA],                    
                C.[HORAINICIO], 
                C.[HORAFIM],
                C.[LINKWEBCONVERSA],
                C.[ID_CONTATOS],
                CO.[NOME], 
                CO.[EMAIL],
                CO.[TELEFONE],
                CO.[EMPRESA],
                CO.[CARGO]
            FROM
                [TBCOMPROMISSO] C LEFT JOIN
                [TBCONTATOS] CO
            ON 
                C.ID_CONTATOS = CO.ID
             WHERE 
                C.[DATAAGENDADA] < GETUTCDATE()";
        private const string sqlSelecionarCompromissosFuturos =
           @"SELECT 
                C.[ID],
                C.[ASSUNTO],       
                C.[LOCAL],             
                C.[DATAAGENDADA],                    
                C.[HORAINICIO], 
                C.[HORAFIM],
                C.[LINKWEBCONVERSA],
                C.[ID_CONTATOS],
                CO.[NOME], 
                CO.[EMAIL],
                CO.[TELEFONE],
                CO.[EMPRESA],
                CO.[CARGO]
            FROM
                [TBCOMPROMISSO] C LEFT JOIN
                [TBCONTATOS] CO
            ON 
                C.ID_CONTATOS = CO.ID
             WHERE 
                C.[DATAAGENDADA] > GETUTCDATE()
             ORDER BY 
				C.[DATAAGENDADA] ASC ";
        
        #endregion

        public override string Editar(int id, Compromisso registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarCompromisso, ObtemParametrosCompromisso(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                Db.Delete(sqlExcluirCompromisso, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return Db.Exists(sqlExisteCompromisso, AdicionarParametro("ID", id));
        }

        public override string InserirNovo(Compromisso registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirCompromisso,ObtemParametrosCompromisso(registro));
            }

            return resultadoValidacao;
        }

        public Compromisso SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarCompromissoPorId, ConverterEmCompromisso, AdicionarParametro("ID", id));
        }
        public override List<Compromisso> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosOsCompromisso, ConverterEmCompromisso);
        }

        public List<Compromisso> SelecionarTodosCompromissosPassados()
        {
            return Db.GetAll(sqlSelecionarCompromissosPassados, ConverterEmCompromisso);
        }

        public List<Compromisso> SelecionarTodosCompromissosPendentes()
        {
            return Db.GetAll(sqlSelecionarCompromissosFuturos, ConverterEmCompromisso);
        }

        private Compromisso ConverterEmCompromisso(IDataReader reader)
        {
                var assunto = Convert.ToString(reader["ASSUNTO"]);
                var local = Convert.ToString(reader["LOCAL"]);
                var dataagendada = Convert.ToDateTime(reader["DATAAGENDADA"]);
                var horainicio = Convert.ToDateTime(reader["HORAINICIO"]);
                var horafim = Convert.ToDateTime(reader["HORAFIM"]);
                var linkweb = Convert.ToString(reader["LINKWEBCONVERSA"]);
                var idcontato = 0;
             if(reader["ID_CONTATOS"] != DBNull.Value)   
                   idcontato = Convert.ToInt32(reader["ID_CONTATOS"]);
               
                var nome = Convert.ToString(reader["NOME"]);
                var email = Convert.ToString(reader["EMAIL"]);
                var telefone = Convert.ToString(reader["TELEFONE"]);
                var empresa = Convert.ToString(reader["EMPRESA"]);
                var cargo = Convert.ToString(reader["CARGO"]);

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);

            contato.Id = idcontato;

            Compromisso compromiso = new Compromisso(assunto, local, dataagendada, horainicio, horafim, linkweb, contato);

            compromiso.Id = Convert.ToInt32(reader["ID"]);

            return compromiso;

        }

        private Dictionary<string, object> ObtemParametrosCompromisso(Compromisso compromisso)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", compromisso.Id);
            parametros.Add("ASSUNTO", compromisso.Assunto);
            parametros.Add("LOCAL", compromisso.Local);
            parametros.Add("DATAAGENDADA", compromisso.DataAgenda);
            parametros.Add("HORAINICIO", compromisso.HoraInicio);
            parametros.Add("HORAFIM", compromisso.HoraFim);
            parametros.Add("LINKWEBCONVERSA", compromisso.LinkWEB);

            if (compromisso.Contato != null)
                parametros.Add("ID_CONTATOS", compromisso.Contato.Id);
            else
            parametros.Add("ID_CONTATOS", DBNull.Value);

            return parametros;
        }
        private static Dictionary<string, object> AdicionarParametro(string campo, int valor)
        {
            return new Dictionary<string, object>() { { campo, valor } };
        }
    }
}
