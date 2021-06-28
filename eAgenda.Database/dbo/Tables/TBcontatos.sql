CREATE TABLE [dbo].[TBcontatos] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (100) NOT NULL,
    [Email]    VARCHAR (200) NOT NULL,
    [Telefone] VARCHAR (50)  NOT NULL,
    [Empresa]  VARCHAR (100) NOT NULL,
    [Cargo]    VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TBcontatos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

