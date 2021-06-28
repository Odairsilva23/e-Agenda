using System.ComponentModel;

namespace eAgenda.Dominio
{
    public enum StatusEnum
    {
        [Description("Tarefa Pendente")]
        Pendente,

        [Description("Tarefa Finalizada")]
        Finalizada
    }
}