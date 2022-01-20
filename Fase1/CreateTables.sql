USE L51NG1
GO

SET XACT_ABORT ON
BEGIN TRANSACTION


CREATE TABLE dbo.Competencias (
	id int NOT NULL ,
	descricao varchar(50) NOT NULL
	primary key (id)
);

CREATE TABLE dbo.Funcionarios (
	id int NOT NULL, 
	nome varchar(50) not null,
	cc int not null unique check (len(cc) = 8),
	nif int not null unique check (nif like '1________' or nif like '2________' ),
	dtNasc date not null, 
	endereco varchar (50) not null, 
	email varchar (50) check(email like ('%@%.%')),
	ntelefone nvarchar(50) check(ntelefone like ('[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')), 
	profissao varchar(50),
	primary key (id),
	UNIQUE (email,cc,nif),

);

CREATE TABLE dbo.FunCompet (
	idFunc int not null,
	idComp int not null,
	PRIMARY KEY (idFunc, idComp),
	FOREIGN KEY (idFunc) REFERENCES Funcionarios(id),
	FOREIGN KEY (idComp) REFERENCES Competencias(id),
);

CREATE TABLE dbo.Tipos (
	id int NOT NULL,
	descricao varchar(50) not null
	primary key (id)
);


CREATE TABLE dbo.Ativos (
	id int NOT NULL,
	nome varchar(50) not null,
	valor money, 
	dtAquisicao date not null, 
	estado bit,
	marca varchar(50),
	modelo varchar(50),
	localizacao varchar(50) not null,
	parentId int,
	tipoId int NOT NULL,
	gestorId int not null,
	PRIMARY KEY (id),
	FOREIGN KEY (parentId) REFERENCES Ativos(id),
	FOREIGN KEY (tipoId) REFERENCES Tipos(id),
	FOREIGN KEY (gestorId) REFERENCES Funcionarios(id),
);

CREATE TABLE dbo.Historico (
	idAtivo int not null,
	dtAlteracao datetime not null,
	valor money,
	PRIMARY KEY (idAtivo,dtAlteracao),
	FOREIGN KEY (idAtivo) REFERENCES Ativos(id),
);

CREATE TABLE dbo.Equipa(
	id int not null,
	localizacao varchar(50),
	nFunc int,
	PRIMARY KEY (id),
);

CREATE TABLE dbo.EquipaFunc (
	funcId int not null,
	equipaId int, 
	supervisor int,
	PRIMARY KEY (funcId),
	FOREIGN KEY (funcId) REFERENCES Funcionarios(id),
	FOREIGN KEY (equipaId) REFERENCES Equipa(id),
	FOREIGN KEY (supervisor) REFERENCES Funcionarios(id),
);

CREATE TABLE dbo.Intervencao (
	id int not null , 
	descricao varchar (50) not null CHECK (descricao in ('avaria','rutura','inspecção')),
	estado varchar (50) not null CHECK (estado in('por atribuir','em análise','em execução','concluído')),
	dtInicio date NOT NULL, 
	dtFim date,
	valor money,
	ativoId int not null,
	PRIMARY KEY (id),
	FOREIGN KEY (ativoId) REFERENCES Ativos(id),
	constraint finalh check( 
						dtFim >= dtInicio 
						),
);

CREATE TABLE dbo.IntervencaoPeriodica (
	id int not null,
	meses int not null, 
	PRIMARY KEY (id),
	FOREIGN KEY (id) REFERENCES Intervencao(id),
);

CREATE TABLE dbo.EquipaIntervencao (
	idIntervencao int not null,
	equipaId int, 
	PRIMARY KEY (idIntervencao),
	FOREIGN KEY (idIntervencao) REFERENCES Intervencao(id),
	FOREIGN KEY (equipaId) REFERENCES Equipa(id),
);

CREATE TABLE dbo.HistAlteracaoEqInterv (
	idIntervencao int not null,
	equipaId int, 
	dtAtualizacao DATETIME, 
	PRIMARY KEY (idIntervencao,dtAtualizacao),
	FOREIGN KEY (idIntervencao) REFERENCES EquipaIntervencao(idIntervencao),
);

COMMIT TRANSACTION

SET XACT_ABORT OFF

GO

--Contagem de nFunc por equipa a em cada update da tabela equipa Func
CREATE OR ALTER  TRIGGER [dbo].[trg_updateNfunc] 
ON [dbo].[EquipaFunc]
after update
AS 
BEGIN
			DECLARE @funcId int,
			@equipaID int,
			@supervisorID int,
			@nFunc INT ,
			@nFuncSup INT

			SELECT @funcId = funcId , @equipaID = equipaID , @nFunc = ef.CONT , @nFuncSup = CONTS , @supervisorID = supervisor
			FROM inserted
				LEFT JOIN (SELECT EquipaId as eid,COUNT(1) AS CONT from EquipaFunc GROUP BY EquipaId )ef on ef.eid = equipaId
				LEFT JOIN (SELECT EquipaId as eids,COUNT(DISTINCT supervisor) AS CONTS from EquipaFunc GROUP BY EquipaId ) efs on efs.eids = equipaId
			Update Equipa set nFunc = ISNULL(@nFunc,0) + iSNULL(@nFuncSup,0) where id = @equipaID
			UPDATE EquipaFunc set equipaId = @equipaID, supervisor = @supervisorID where funcId = @supervisorID

			SELECT @funcId = funcId , @equipaID = equipaID , @nFunc = ef.CONT , @nFuncSup = CONTS
			FROM deleted
				LEFT JOIN (SELECT EquipaId as eid,COUNT(1) AS CONT from EquipaFunc GROUP BY EquipaId )ef on ef.eid = equipaId
				LEFT JOIN (SELECT EquipaId as eids,COUNT(DISTINCT supervisor) AS CONTS from EquipaFunc GROUP BY EquipaId ) efs on efs.eids = equipaId
			Update Equipa set nFunc = ISNULL(@nFunc,0) + iSNULL(@nFuncSup,0) where id = @equipaID
END


GO 
--Elimina da tabela funcionarios e equipa funcionarios
CREATE OR ALTER TRIGGER [trg_deletefunc]
ON [dbo].[EquipaFunc]
FOR DELETE
AS 
BEGIN
	declare @funcionario int,
			@equipa int, 
			@supervidorId int,
			@nFunc INT ,
			@nFuncSup INT

	SELECT @funcionario = funcId , @equipa = equipaID , @nFunc = ef.CONT , @nFuncSup = CONTS
	FROM deleted
				LEFT JOIN (SELECT EquipaId as eid,COUNT(1) AS CONT from EquipaFunc GROUP BY EquipaId )ef on ef.eid = equipaId
				LEFT JOIN (SELECT EquipaId as eids,COUNT(DISTINCT supervisor) AS CONTS from EquipaFunc GROUP BY EquipaId ) efs on efs.eids = equipaId
	Update Equipa set nFunc = ISNULL(@nFunc,0) + iSNULL(@nFuncSup,0) where id = @equipa


	UPDATE EquipaFunc SET @supervidorId = NULL WHERE supervisor = @funcionario
	DELETE FROM EquipaFunc WHERE funcId = @funcionario
	DELETE FROM Funcionarios WHERE id = @funcionario
	DELETE FROM FunCompet WHERE idFunc = @funcionario


END
GO

GO
--Elimina da tabela equipa Intervençao e 
CREATE OR ALTER TRIGGER [dbo].[trg_DeleteEquipaIntervencao] 
ON [dbo].[EquipaIntervencao]
instead of DELETE
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int,
			@Data datetime = GETDATE()
			SELECT @Intervencao = idIntervencao , @Equipa = equipaId FROM deleted
		
		DELETE FROM dbo.HistAlteracaoEqInterv where @Intervencao = idIntervencao
		DELETE FROM dbo.EquipaIntervencao where @Intervencao = idIntervencao
		DELETE FROM dbo.IntervencaoPeriodica where @Intervencao = id
		DELETE FROM dbo.Intervencao where @Intervencao = id
END

GO
--trigger com regras de atribuição de equipa 
CREATE OR ALTER TRIGGER [dbo].[trg_UpdateEquipaIntervencao] 
ON [dbo].[EquipaIntervencao]
AFTER UPDATE 
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int,
			@Data datetime = GETDATE()
			SELECT @Intervencao = idIntervencao, @Equipa = equipaId FROM inserted
--não pode ter menos de dois funcionarios sendo um deles supervisor
IF EXISTS (	
			Select * 
			from [EquipaIntervencao] ei
				inner join inserted i on i.idIntervencao = ei.idIntervencao
				LEFT JOIN Equipa e on e.id = ei.equipaId
			where e.nFunc < 2 OR E.id IN (SELECT equipaId from EquipaFunc where supervisor = 0)
			)
	BEGIN 
		RAISERROR ('Equipa tem de ter pelo menos 2 funcionarios sendo um deles o supervisor', 16, 1);
	END

--não pode ter mais de 3 intervenções atribuidas e apenas uma em curso

IF ( (select COUNT (ei.equipaId) 
	 from EquipaIntervencao ei
	 inner join inserted on ei.equipaId = inserted.equipaId
	 inner join Intervencao i ON I.id = ei.idIntervencao and i.estado in ('em análise','em execução')) > 3)
	BEGIN 
		RAISERROR ('Limite de intervençoes atingida', 16, 1);
	END

IF ( (select COUNT (ei.equipaId) 
	 from EquipaIntervencao ei
	 inner join inserted on ei.equipaId = inserted.equipaId
	 inner join Intervencao i ON I.id = ei.idIntervencao and i.estado in ('em execução')) >= 2)

	BEGIN 
		RAISERROR ('Limite de intervençoes em execucao atingida', 16, 1);
	END


--a pessoa que gere o ativo nao pode participar na intervencao
if exists (
			 SELECT DISTINCT funcId from EquipaFunc
			inner join Ativos on Ativos.gestorId = EquipaFunc.funcId
			inner join Intervencao on Intervencao.ativoId = Ativos.id AND Intervencao.id = @Intervencao
			inner join inserted on inserted.equipaId = EquipaFunc.equipaId
			)
	BEGIN 
		RAISERROR ('Pessoa que gere o ativo nao pode pertencer à equipa', 16, 1);
	END

--a descricao da intervencao tem de ser compativel com  as competencias 
			
IF NOT EXISTS (
			select descricao
			from equipaFunc EF
			inner join FunCompet FC on FC.idFunc = EF.funcId
			inner join Competencias c on c.id = FC.idComp
			INNER JOIN inserted I ON I.equipaId = EF.equipaId
			where EF.equipaId = @Equipa and descricao in (SELECT descricao FROM Intervencao where id = @Intervencao)
			group by EF.equipaId, descricao
)
	BEGIN 
		RAISERROR ('Equipa nao tem competencias', 16, 1);
	END

-- EQUIPA ATRIBUIDA
			update Intervencao set estado = 'em análise' where Intervencao.id = @Intervencao
			insert into HistAlteracaoEqInterv VALUES (@Intervencao, @Equipa, @Data)
END

GO


--Insere na tabela equipaFun um funcionario sem equipa
CREATE OR ALTER  TRIGGER [dbo].[trg_InsertmembroEquipa] 
ON [dbo].[Funcionarios]
FOR INSERT 
AS 
BEGIN
			DECLARE @funcId int
			SELECT @funcId = id FROM inserted
			INSERT INTO EquipaFunc VALUES (@funcId, null,null)
END

go
--antes de atualizar estado, verifica se a equipa atribuida tem mais intervencoes em execucao 
CREATE OR ALTER TRIGGER [dbo].[trg_updateEstado]
ON [dbo].[Intervencao]
AFTER UPDATE
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int
			SELECT @Intervencao = id, @Equipa = ei.equipaId from inserted
				left join EquipaIntervencao ei on ei.idIntervencao = id
IF Update(estado)
	BEGIN
		if ((select count(I.ID) 
			from EquipaIntervencao ei
			inner join Intervencao i ON i.id = ei.idIntervencao and i.estado = 'em execução' 
			where equipaId = @Equipa) > 1)
			BEGIN
				RAISERROR ('Equipa já tem uma intervenção em execução', 16, 1);
			END
	END
END

GO
-- insere na tabela de historico de ativos
CREATE OR ALTER TRIGGER [dbo].[trg_HistoricoValorAtivo]
ON [dbo].[Ativos]
AFTER INSERT, UPDATE
AS 
IF(update(valor))
BEGIN
			DECLARE @ativoValor money, 
		    		@ativoId int,
				@data datetime = GETDATE()
			SELECT @ativoId = id , @ativoValor = valor FROM inserted

	INSERT INTO dbo.Historico values (@ativoId,@data, @ativoValor)
END

go
-- insere na tabela de equipaInter de ativos
CREATE TRIGGER dbo.trg_insertInter
   ON  dbo.[Intervencao]
AFTER INSERT
AS 
BEGIN
	declare @IntervencaoID int

	SELECT @IntervencaoID = id from inserted
	INSERT INTO EquipaIntervencao VALUES (@IntervencaoID,null)
END
GO