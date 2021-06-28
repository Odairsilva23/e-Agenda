CREATE TABLE [dbo].[TBCompromisso] (
    [Id]              INT      IDENTITY  (1, 1)   NOT NULL,
    [Assunto]         VARCHAR (100) NOT NULL,
    [Local]           VARCHAR (50)  NOT NULL,
    [DataAgendada]    DATETIME          NOT NULL,
    [HoraInicio]      DATETIME      NOT NULL,
    [HoraFim]         DATETIME      NOT NULL,
    [LinkWebConversa] TEXT          NULL,
    [Id_Contatos]     INT           NULL,
    CONSTRAINT [PK_TBCompromisso] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBCompromisso_TBcontatos] FOREIGN KEY ([Id_Contatos]) REFERENCES [dbo].[TBcontatos] ([Id])
);

