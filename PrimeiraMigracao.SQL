IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [Nome] VARCHAR(80) NOT NULL,
    [Fone] CHAR(11) NULL,
    [CEP] CHAR(8) NULL,
    [Estado]CHAR(2) NULL,
    [Cidade] VARCHAR(60) NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [CodigoBarras] VARCHAR(14) NULL,
    [Descricao] VARCHAR(60) NULL,
    [Valor] DECIMAL(18,2) NOT NULL,
    [TipoProduto] INT NOT NULL,
    [Ativo] BIT NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pedidos] (
    [Id] int NOT NULL IDENTITY,
    [ClientesId] int  NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [IniciadoEm] datetime2 NOT NULL,
    [FinalizadoEm] datetime2 NOT NULL,
    [TipoFrete] int NOT NULL,
    [Status] int NOT NULL,
    [Observacao] VARCHAR(512) NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedidos_Clientes_ClienteId] FOREIGN KEY ([ClientesId]) REFERENCES [Clientes] ([Id])
);
GO

CREATE TABLE [PedidoItem] (
    [Id] int NOT NULL IDENTITY,
    [PedidosId] INT NOT NULL,
    [ProdutosId] INT NOT NULL,
    [Quantidade] INT NOT NULL,
    [Valor] DECIMAL(18,2) NOT NULL,
    [Desconto] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_PedidoItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItem_Pedidos_PedidosId] FOREIGN KEY ([PedidosId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PedidoItem_Produtos_ProdutosId] FOREIGN KEY ([ProdutosId]) REFERENCES [Produtos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_PedidoItem_PedidosId] ON [PedidoItem] ([PedidosId]);
GO

CREATE INDEX [IX_PedidoItem_ProdutosId] ON [PedidoItem] ([ProdutosId]);
GO

CREATE INDEX [IX_Pedidos_ClientesId] ON [Pedidos] ([ClientesId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231019022013_PrimeiraMigracao', N'7.0.12');
GO

COMMIT;
GO

