/*
	==============================================
	Classe     : Script para Gerar Nova Base de Dados 
	Descrição  : Este script gera uma nova Base de Dados do ETL Starload
	Criado por : Carlos R. S. Oliveira
	Criado em  : 20/05/2019
	Versao     : 1.0
	==============================================
*/
/*Criação da Nova Base*/
USE [master]
GO
CREATE DATABASE [BaseStarETL]
GO
/*Fim da criação da Nova Base*/

/*Criação das Tabelas na Nova Base*/
USE [BaseStarETL]
GO
/****** Object:  Table [dbo].[tb_arquivos]    Script Date: 21/10/2019 10:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[tb_arquivos]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_arquivos](
	[id_arquivo] [int] IDENTITY(1,1) NOT NULL,
	[id_pacote] [int] NOT NULL,
	[nm_arquivo] [nvarchar](128) NOT NULL,
	[mascara_arquivo] [nvarchar](64) NOT NULL,
	[tp_carga] [nvarchar](16) NOT NULL,
	[tp_arquivo] [nvarchar](16) NOT NULL,
	[delimitador] [nvarchar](5) NULL,
	[cercador] [nvarchar](1) NULL,
	[tb_destino] [nvarchar](64) NULL,
	[dir_entrada] [nvarchar](128) NOT NULL,
	[dir_saida] [nvarchar](128) NULL,
	[rbd_tabela] [nvarchar](16) NOT NULL,
	[rbd_indice] [nvarchar](16) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_arquivos_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_arquivos_data_atualizacao]  DEFAULT (getdate()),
	[nm_Planilha] [nvarchar](128) NULL,
	[ArqExcluido] [char](3) NULL DEFAULT ('NÃO'),
	[LineFeed] [nvarchar](4) NULL,
	[FirstLine] [int] NULL DEFAULT ((1)),
	[ConexaoBusiness] [nvarchar](50) NULL,
 CONSTRAINT [PK_tb_arquivos] PRIMARY KEY CLUSTERED 
(
	[id_arquivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_backup]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_backup](
	[Id_backup] [int] IDENTITY(1,1) NOT NULL,
	[ID_Processo] [int] NOT NULL,
	[NomeApp] [nvarchar](100) NULL,
	[PathBackup] [nvarchar](100) NULL,
	[Nomebackup] [nvarchar](30) NULL,
	[Data_Criacao] [datetime] NULL,
	[Mensagem] [nvarchar](4000) NULL,
	[Status] [nvarchar](20) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_categorias]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_categorias](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[id_grupo] [int] NOT NULL,
	[nm_categoria] [nvarchar](64) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_categorias_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_categorias_data_atualizacao]  DEFAULT (getdate()),
	[DESCR_CATEGORIAS] [nvarchar](128) NULL,
	[Smtp_Host] [nvarchar](100) NULL,
	[Smtp_Port] [int] NULL DEFAULT ((587)),
	[Smtp_EnableSSL] [char](3) NULL,
	[Smtp_eMail] [nvarchar](100) NULL,
	[Smtp_Senha] [nvarchar](255) NULL,
	[EnviarSoErros] [char](3) NULL DEFAULT ('SIM'),
 CONSTRAINT [PK_tb_categorias] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_grupos]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_grupos](
	[id_grupo] [int] IDENTITY(1,1) NOT NULL,
	[nm_grupo] [nvarchar](64) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_grupos_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_grupos_data_atualizacao]  DEFAULT (getdate()),
	[DESCR_GRUPOS] [nvarchar](128) NULL,
	[Site_Empresa] [nvarchar](128) NULL,
	[Logo_Empresa] [nvarchar](128) NULL,
 CONSTRAINT [PK_tb_grupos] PRIMARY KEY CLUSTERED 
(
	[id_grupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_indices]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_indices](
	[id_indice] [int] IDENTITY(1,1) NOT NULL,
	[id_arquivo] [int] NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL,
	[data_atualizacao] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_indices] PRIMARY KEY CLUSTERED 
(
	[id_indice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_logs]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_logs](
	[id_arquivo] [int] NOT NULL,
	[data] [datetime] NOT NULL,
	[etapa] [nvarchar](16) NOT NULL,
	[Mensagem] [nvarchar](4000) NULL,
	[ComandoSql] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_mapeamentos]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_mapeamentos](
	[id_mapeamento] [int] IDENTITY(1,1) NOT NULL,
	[id_arquivo] [int] NOT NULL,
	[nm_coluna] [nvarchar](32) NOT NULL,
	[ordem] [int] NOT NULL,
	[fixo_inicio] [int] NULL,
	[fixo_tamanho] [int] NULL,
	[id_indice] [int] NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_mapeamentos_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_mapeamentos_data_atualizacao]  DEFAULT (getdate()),
	[tp_coluna] [nvarchar](32) NULL,
	[tm_coluna] [int] NULL,
	[pr_coluna] [int] NULL,
	[MASK_CAMPO] [nvarchar](30) NULL,
	[ExpressaoSql] [nvarchar](4000) NULL,
 CONSTRAINT [PK_tb_mapeamentos] PRIMARY KEY CLUSTERED 
(
	[id_mapeamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_monitoramentos]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_monitoramentos](
	[id_servico] [int] NOT NULL,
	[id_arquivo] [int] NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_monitoramentos_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_monitoramentos_data_atualizacao]  DEFAULT (getdate()),
 CONSTRAINT [PK_tb_monitoramentos] PRIMARY KEY CLUSTERED 
(
	[id_servico] ASC,
	[id_arquivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_objetos]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_objetos](
	[id_objeto] [int] IDENTITY(1,1) NOT NULL,
	[nm_objeto] [nvarchar](128) NOT NULL,
	[tp_objeto] [nvarchar](64) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_objetos_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_objetos_data_atualizacao]  DEFAULT (getdate()),
 CONSTRAINT [PK_tb_objetos] PRIMARY KEY CLUSTERED 
(
	[id_objeto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_pacotes]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_pacotes](
	[id_pacote] [int] IDENTITY(1,1) NOT NULL,
	[id_categoria] [int] NOT NULL,
	[nm_pacote] [nvarchar](64) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_pacotes_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_pacotes_data_atualizacao]  DEFAULT (getdate()),
	[DESCR_PACOTES] [nvarchar](128) NULL,
 CONSTRAINT [PK_tb_pacotes] PRIMARY KEY CLUSTERED 
(
	[id_pacote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_parametros]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_parametros](
	[id_parametro] [int] IDENTITY(1,1) NOT NULL,
	[nm_parametro] [nvarchar](64) NOT NULL,
	[valor] [nvarchar](256) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_parametros_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_parametros_data_atualizacao]  DEFAULT (getdate()),
 CONSTRAINT [PK_tb_parametros] PRIMARY KEY CLUSTERED 
(
	[id_parametro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_perfis]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_perfis](
	[id_perfil] [int] IDENTITY(1,1) NOT NULL,
	[nm_perfil] [nvarchar](64) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF__tb_perfis__data___70DDC3D8]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF__tb_perfis__data___71D1E811]  DEFAULT (getdate()),
 CONSTRAINT [PK_tb_perfis] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_permissoes]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_permissoes](
	[id_permissao] [int] IDENTITY(1,1) NOT NULL,
	[id_perfil] [int] NOT NULL,
	[id_objeto] [int] NOT NULL,
	[permissao] [nvarchar](32) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF__tb_permis__data___797309D9]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF__tb_permis__data___7A672E12]  DEFAULT (getdate())
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_rejeicoes]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_rejeicoes](
	[id_arquivo] [int] NOT NULL,
	[linha_original] [nvarchar](2048) NOT NULL,
	[comando_importacao] [nvarchar](2048) NOT NULL,
	[data_importacao] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_servicos]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_servicos](
	[id_servico] [int] IDENTITY(1,1) NOT NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF_tb_servicos_data_criacao]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF_tb_servicos_data_atualizacao]  DEFAULT (getdate()),
	[NM_SERVICO] [nvarchar](128) NULL,
	[Situacao] [nvarchar](15) NULL,
 CONSTRAINT [PK_tb_servicos] PRIMARY KEY CLUSTERED 
(
	[id_servico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_usuarios]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_perfil] [int] NOT NULL,
	[nm_usuario] [nvarchar](128) NOT NULL,
	[login] [nvarchar](32) NOT NULL,
	[senha] [nvarchar](128) NOT NULL,
	[email] [nvarchar](64) NULL,
	[status] [nvarchar](16) NOT NULL,
	[data_criacao] [datetime] NOT NULL CONSTRAINT [DF__tb_usuari__data___7F2BE32F]  DEFAULT (getdate()),
	[data_atualizacao] [datetime] NOT NULL CONSTRAINT [DF__tb_usuari__data___00200768]  DEFAULT (getdate()),
	[RecebeNotificacao] [char](3) NULL DEFAULT ('SIM'),
 CONSTRAINT [PK_tb_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_UsuariosXCategorias]    Script Date: 20/01/2020 15:23:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_UsuariosXCategorias](
	[id_UsuCateg] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_categoria] [int] NULL,
	[data_criacao] [datetime] NULL DEFAULT (getdate()),
	[data_atualizacao] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tb_UsuariosXCategorias] PRIMARY KEY CLUSTERED 
(
	[id_UsuCateg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tb_backup] ADD  DEFAULT (getdate()) FOR [Data_Criacao]
GO
ALTER TABLE [dbo].[tb_indices] ADD  CONSTRAINT [DF_tb_indices_data_criacao]  DEFAULT (getdate()) FOR [data_criacao]
GO
ALTER TABLE [dbo].[tb_indices] ADD  CONSTRAINT [DF_tb_indices_data_atualizacao]  DEFAULT (getdate()) FOR [data_atualizacao]
GO
ALTER TABLE [dbo].[tb_arquivos]  WITH CHECK ADD  CONSTRAINT [FK_tb_arquivos_tb_pacotes] FOREIGN KEY([id_pacote])
REFERENCES [dbo].[tb_pacotes] ([id_pacote])
GO
ALTER TABLE [dbo].[tb_arquivos] CHECK CONSTRAINT [FK_tb_arquivos_tb_pacotes]
GO
ALTER TABLE [dbo].[tb_categorias]  WITH CHECK ADD  CONSTRAINT [FK_tb_categorias_tb_grupos] FOREIGN KEY([id_grupo])
REFERENCES [dbo].[tb_grupos] ([id_grupo])
GO
ALTER TABLE [dbo].[tb_categorias] CHECK CONSTRAINT [FK_tb_categorias_tb_grupos]
GO
ALTER TABLE [dbo].[tb_indices]  WITH CHECK ADD  CONSTRAINT [FK_tb_indices_tb_arquivos] FOREIGN KEY([id_arquivo])
REFERENCES [dbo].[tb_arquivos] ([id_arquivo])
GO
ALTER TABLE [dbo].[tb_indices] CHECK CONSTRAINT [FK_tb_indices_tb_arquivos]
GO
ALTER TABLE [dbo].[tb_mapeamentos]  WITH CHECK ADD  CONSTRAINT [FK_tb_mapeamentos_tb_arquivos] FOREIGN KEY([id_arquivo])
REFERENCES [dbo].[tb_arquivos] ([id_arquivo])
GO
ALTER TABLE [dbo].[tb_mapeamentos] CHECK CONSTRAINT [FK_tb_mapeamentos_tb_arquivos]
GO
ALTER TABLE [dbo].[tb_mapeamentos]  WITH CHECK ADD  CONSTRAINT [FK_tb_mapeamentos_tb_indices] FOREIGN KEY([id_indice])
REFERENCES [dbo].[tb_indices] ([id_indice])
GO
ALTER TABLE [dbo].[tb_mapeamentos] CHECK CONSTRAINT [FK_tb_mapeamentos_tb_indices]
GO
ALTER TABLE [dbo].[tb_monitoramentos]  WITH CHECK ADD  CONSTRAINT [FK_tb_monitoramentos_tb_arquivos] FOREIGN KEY([id_arquivo])
REFERENCES [dbo].[tb_arquivos] ([id_arquivo])
GO
ALTER TABLE [dbo].[tb_monitoramentos] CHECK CONSTRAINT [FK_tb_monitoramentos_tb_arquivos]
GO
ALTER TABLE [dbo].[tb_monitoramentos]  WITH CHECK ADD  CONSTRAINT [FK_tb_monitoramentos_tb_servicos] FOREIGN KEY([id_servico])
REFERENCES [dbo].[tb_servicos] ([id_servico])
GO
ALTER TABLE [dbo].[tb_monitoramentos] CHECK CONSTRAINT [FK_tb_monitoramentos_tb_servicos]
GO
ALTER TABLE [dbo].[tb_pacotes]  WITH CHECK ADD  CONSTRAINT [FK_tb_pacotes_tb_categorias] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[tb_categorias] ([id_categoria])
GO
ALTER TABLE [dbo].[tb_pacotes] CHECK CONSTRAINT [FK_tb_pacotes_tb_categorias]
GO
ALTER TABLE [dbo].[tb_permissoes]  WITH CHECK ADD  CONSTRAINT [FK_tb_permissoes_tb_objetos] FOREIGN KEY([id_objeto])
REFERENCES [dbo].[tb_objetos] ([id_objeto])
GO
ALTER TABLE [dbo].[tb_permissoes] CHECK CONSTRAINT [FK_tb_permissoes_tb_objetos]
GO
ALTER TABLE [dbo].[tb_permissoes]  WITH CHECK ADD  CONSTRAINT [FK_tb_permissoes_tb_perfis] FOREIGN KEY([id_perfil])
REFERENCES [dbo].[tb_perfis] ([id_perfil])
GO
ALTER TABLE [dbo].[tb_permissoes] CHECK CONSTRAINT [FK_tb_permissoes_tb_perfis]
GO
ALTER TABLE [dbo].[tb_rejeicoes]  WITH CHECK ADD  CONSTRAINT [FK_tb_rejeicoes_tb_arquivos] FOREIGN KEY([id_arquivo])
REFERENCES [dbo].[tb_arquivos] ([id_arquivo])
GO
ALTER TABLE [dbo].[tb_rejeicoes] CHECK CONSTRAINT [FK_tb_rejeicoes_tb_arquivos]
GO
ALTER TABLE [dbo].[tb_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_tb_usuarios_tb_perfis] FOREIGN KEY([id_perfil])
REFERENCES [dbo].[tb_perfis] ([id_perfil])
GO
ALTER TABLE [dbo].[tb_usuarios] CHECK CONSTRAINT [FK_tb_usuarios_tb_perfis]
GO
ALTER TABLE [dbo].[tb_UsuariosXCategorias]  WITH CHECK ADD  CONSTRAINT [FK_tb_UsuariosXCategorias_tb_categorias] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[tb_categorias] ([id_categoria])
GO
ALTER TABLE [dbo].[tb_UsuariosXCategorias] CHECK CONSTRAINT [FK_tb_UsuariosXCategorias_tb_categorias]
GO
ALTER TABLE [dbo].[tb_UsuariosXCategorias]  WITH CHECK ADD  CONSTRAINT [FK_tb_UsuariosXCategorias_tb_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[tb_usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_UsuariosXCategorias] CHECK CONSTRAINT [FK_tb_UsuariosXCategorias_tb_usuarios]
GO
ALTER TABLE [dbo].[tb_arquivos]  WITH CHECK ADD  CONSTRAINT [CK_tb_arquivos_rbd_indice] CHECK  (([rbd_indice]='NÃO' OR [rbd_indice]='SIM'))
GO
ALTER TABLE [dbo].[tb_arquivos] CHECK CONSTRAINT [CK_tb_arquivos_rbd_indice]
GO
ALTER TABLE [dbo].[tb_arquivos]  WITH CHECK ADD  CONSTRAINT [CK_tb_arquivos_rbd_tabela] CHECK  (([rbd_tabela]='NÃO' OR [rbd_tabela]='SIM'))
GO
ALTER TABLE [dbo].[tb_arquivos] CHECK CONSTRAINT [CK_tb_arquivos_rbd_tabela]
GO
ALTER TABLE [dbo].[tb_arquivos]  WITH CHECK ADD  CONSTRAINT [CK_tb_arquivos_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_arquivos] CHECK CONSTRAINT [CK_tb_arquivos_status]
GO
ALTER TABLE [dbo].[tb_arquivos]  WITH CHECK ADD  CONSTRAINT [CK_tb_arquivos_tp_arquivo] CHECK  (([tp_arquivo]='EXCEL' OR [tp_arquivo]='DBF' OR [tp_arquivo]='DELIMITADO' OR [tp_arquivo]='FIXO'))
GO
ALTER TABLE [dbo].[tb_arquivos] CHECK CONSTRAINT [CK_tb_arquivos_tp_arquivo]
GO
ALTER TABLE [dbo].[tb_arquivos]  WITH CHECK ADD  CONSTRAINT [CK_tb_arquivos_tp_carga] CHECK  (([tp_carga]='INCREMENTAL' OR [tp_carga]='FULL'))
GO
ALTER TABLE [dbo].[tb_arquivos] CHECK CONSTRAINT [CK_tb_arquivos_tp_carga]
GO
ALTER TABLE [dbo].[tb_categorias]  WITH CHECK ADD  CONSTRAINT [CK_tb_categorias_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_categorias] CHECK CONSTRAINT [CK_tb_categorias_status]
GO
ALTER TABLE [dbo].[tb_grupos]  WITH CHECK ADD  CONSTRAINT [CK_tb_grupos_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_grupos] CHECK CONSTRAINT [CK_tb_grupos_status]
GO
ALTER TABLE [dbo].[tb_indices]  WITH CHECK ADD  CONSTRAINT [CK_tb_indices_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_indices] CHECK CONSTRAINT [CK_tb_indices_status]
GO
ALTER TABLE [dbo].[tb_monitoramentos]  WITH CHECK ADD  CONSTRAINT [CK_tb_monitoramentos_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_monitoramentos] CHECK CONSTRAINT [CK_tb_monitoramentos_status]
GO
ALTER TABLE [dbo].[tb_objetos]  WITH CHECK ADD  CONSTRAINT [CK_tb_objetos_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_objetos] CHECK CONSTRAINT [CK_tb_objetos_status]
GO
ALTER TABLE [dbo].[tb_pacotes]  WITH CHECK ADD  CONSTRAINT [CK_tb_pacotes_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_pacotes] CHECK CONSTRAINT [CK_tb_pacotes_status]
GO
ALTER TABLE [dbo].[tb_parametros]  WITH CHECK ADD  CONSTRAINT [CK_tb_parametros_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_parametros] CHECK CONSTRAINT [CK_tb_parametros_status]
GO
ALTER TABLE [dbo].[tb_perfis]  WITH CHECK ADD  CONSTRAINT [CK_tb_perfis_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_perfis] CHECK CONSTRAINT [CK_tb_perfis_status]
GO
ALTER TABLE [dbo].[tb_servicos]  WITH CHECK ADD  CONSTRAINT [CK_tb_servicos_status] CHECK  (([status]='inativo' OR [status]='ativo'))
GO
ALTER TABLE [dbo].[tb_servicos] CHECK CONSTRAINT [CK_tb_servicos_status]
GO


/****** Dados para a geração do usuário padrão ******/
USE [BaseStarETL]
GO
delete from tb_permissoes
GO
delete from tb_objetos
GO
delete from tb_UsuariosXCategorias
GO
delete from tb_usuarios
GO
delete from tb_perfis
GO

insert into tb_perfis(nm_perfil,status) Values('ADMIN','ativo');
declare @idPerfil int;
set @idPerfil=(select max(id_perfil) from tb_perfis); 
insert into tb_usuarios(id_perfil,nm_usuario,login,senha,email,status) Values(@idPerfil,'Administrador','Administrador','0F71A16B7C085130DAAF9B75673D430B','email@teste.com.br','ativo');
--A senha criptgrafada é : MudarSenha
Declare @idObj int;
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema_Configuracao','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema_Configuracao_SelecionarEmpresa','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema_Usuarios','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema_Usuarios_AlterarSenha','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema_Usuarios_MudarUsuario','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Sistema_Sair','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro_Perfil','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro_Usuario','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro_Permissoes','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro_Empresa','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro_Filial','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Cadastro_Parametros','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('ETL','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('ETL_Servicos','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('ETL_Pacotes','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('ETL_Arquivos','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Processos','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Processos_ExportarTabelas','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Processos_ImportarTabelas','Menu','ativo');
insert into tb_objetos(nm_objeto,tp_objeto,status) Values('Processos_VisualizarAndamento','Menu','ativo');


declare @IdObjeto int;
declare cursor_objeto cursor for select id_objeto from tb_objetos;
open cursor_objeto;
fetch cursor_objeto into @idobjeto;
insert into tb_permissoes(id_perfil,id_objeto,permissao,status) Values(@idPerfil,@idobjeto,'1|1|1|1|1|','ativo');
while (@@FETCH_STATUS<>-1)
begin
	fetch cursor_objeto into @idobjeto;
	insert into tb_permissoes(id_perfil,id_objeto,permissao,status) Values(@idPerfil,@idobjeto,'1|1|1|1|1|','ativo');
end;
close cursor_objeto;
deallocate cursor_objeto;



--Após instalar Microsoft.ACE.OLEDB.12.0 de ser executado os comandos abaixo para que o OPENROWSET funcione
--sp_configure 'show advanced options', 1;  
--RECONFIGURE;
--GO
--sp_configure 'Ad Hoc Distributed Queries', 1;  
--RECONFIGURE;
--GO  

--EXEC StarETL.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0' , N'AllowInProcess' , 1;
--GO
--EXEC StarETL.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0' , N'DynamicParameters' , 1;
--GO

--USE [master] 
--GO 
--EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'AllowInProcess', 1 
--GO 
--EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'DynamicParameters', 1 
--GO 


