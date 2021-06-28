using eAgenda.Dominio;
using System;
using System.Collections.Generic;

namespace eAgenda.Controladores
{
    public abstract class Controlador<T> where T : EntidadeBase
    {
        public abstract string InserirNovo(T registro);
        public abstract string Editar(int id, T registro);
        public abstract bool Existe(int id);
        public abstract bool Excluir(int id);
        public abstract List<T> SelecionarTodos();

        
    }
}