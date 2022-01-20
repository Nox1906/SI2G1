SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Authors:		Diogo Fernandes , Tiago Ribeiro

-- Description:	d) Mecanismo que permite Inserir, remover e atualizar informac˜ao de uma pessoa. N˜ao
-- podem ser usados insert, update e delete directos. (1 script autónomo que contem as
-- alineas 2d a 2k);
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SP_Funcionarios] (@id int,
                                          @nome VARCHAR(50),
                                          @cc int,
                                          @nif int,
                                          @dtNasc date,
                                          @endereco VARCHAR(50),
                                          @email VARCHAR(50),
                                          @ntelefone VARCHAR(50),
                                          @profissao VARCHAR(50),
                                          @operationType NVARCHAR(15))
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
BEGIN TRANSACTION
BEGIN
	IF @operationType = 'Insert'
	BEGIN
		INSERT INTO Funcionarios(id, nome, cc, nif, dtNasc, endereco, email, ntelefone, profissao)
		VALUES (@id, @nome, @cc, @nif,@dtNasc, @endereco, @email, @ntelefone, @profissao)
	END
	IF @operationType = 'Update'
	BEGIN 
		UPDATE Funcionarios
            SET    nome = @nome,
                   cc = @cc,
                   nif = @nif,
                   dtNasc = @dtNasc,
                   endereco = @endereco,
                   email = @email,
                   ntelefone = @ntelefone,
                   profissao= @profissao
            WHERE  id = @id
    END 
    ELSE IF @operationType = 'Delete'
    BEGIN
		DELETE FROM EquipaFunc WHERE funcId = @id
	END
COMMIT
END

GO
-- =============================================
--e ) Obter o cOodigo de uma equipa livre, dada uma descri¸c˜ao de interven¸c˜ao, capaz de
--resolver o problema. Em caso de haver v´arias equipas deve escolher-se a que teve uma
--intervenãao atribuida á mais tempo;
-- =============================================
CREATE or alter FUNCTION dbo.F_ObterEquipaLivre (@competencia varchar(50))
RETURNS INT 
AS
BEGIN
	declare @equipa int

		SELECT top (1) @equipa = ef.equipaId from dbo.FunCompet fc
	inner join dbo.Competencias c on c.descricao = @competencia and fc.idComp = c.id
	left join dbo.EquipaFunc ef on ef.funcId = fc.idFunc
	LEFT JOIN EquipaIntervencao ei on ei.equipaId = ef.equipaId
	left join HistAlteracaoEqInterv ha on ha.equipaId = ei.equipaId
	left JOIN (	select equipaId, count(idIntervencao) as contagemIntevencoes
					from EquipaIntervencao 
					LEFT JOIN Intervencao i ON I.id = EquipaIntervencao.idIntervencao
					WHERE equipaId is not null and equipaId not in 	(select ei.equipaId from EquipaIntervencao ei
														inner join Intervencao i on i.id = ei.idIntervencao 
														and estado = 'em execução')	GROUP BY equipaId
														having count(idIntervencao) < 3
) sub on sub.equipaId = ei.equipaId
order by ISNULL(ha.dtAtualizacao,0)
RETURN @equipa
end


GO
/****** Object:  StoredProcedure [dbo].[SP_criaInter]    Script Date: 03/12/2021 15:59:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	f) Criar o procedimento para criaInter que permite criar uma intervencao;
-- =============================================
create or ALTER  PROCEDURE [dbo].[SP_criaInter]
	@id int,
	@descricao varchar(50),
	@dtInicio date,
	@dtFim date,
	@valor money,
	@ativoId int,
	@meses int

AS	
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  --Apenas estou a ler uma vez
BEGIN TRANSACTION
if ((SELECT dtAquisicao FROM DBO.Ativos WHERE ativos.id = @ativoId) < @dtInicio)
	begin

		INSERT INTO dbo.Intervencao VALUES(@id,@descricao,'por atribuir',@dtInicio,@dtFim,@valor,@ativoId)
		if (@meses > 0)
			INSERT INTO dbo.IntervencaoPeriodica values (@id, @meses)
		COMMIT
	END
ELSE
	BEGIN
		RAISERROR('Data da Intervencao menor que a data de aquisicao, linha não foi inserida',16,1)
		ROLLBACK
	END

-- =============================================
-- Description:	g) Implementar o mecanismo que permite criar uma equipa;
-- =============================================
GO
CREATE OR ALTER PROCEDURE [dbo].[SP_criaEquipa] (@idEquipa int, @localizacao varchar(50))
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN
	BEGIN TRANSACTION
		INSERT INTO dbo.Equipa VALUES (@idEquipa, @localizacao, 0)
	COMMIT
END

-- =============================================
-- Description:	h) Actualizar (adicionar ou remover) os elementos de uma equipe e associar as respectivas competências;
-- =============================================
GO
--atualizar membros da equuipa
CREATE OR ALTER PROCEDURE [dbo].[SP_ActualizarElementosEquipa] (@equipaId int, @FuncId int, @operationType varchar(20), @supervisor INT)
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
BEGIN

IF (not EXISTS (SELECT * FROM EquipaFunc WHERE funcId = @FuncId) or NOT EXISTS (SELECT * FROM Equipa WHERE id = @equipaId))
	BEGIN
		RAISERROR('Funcionario/Equipa nao existe',16,1)
	END 
ELSE 
	BEGIN 
		BEGIN TRANSACTION
		IF (@operationType = 'Insert')
			BEGIN
				IF (@supervisor = 0)
					UPDATE dbo.EquipaFunc SET equipaId = @equipaId WHERE funcId = @FuncId
				else
					begin
						UPDATE dbo.EquipaFunc SET equipaId = @equipaId, supervisor = @supervisor WHERE funcId = @FuncId
					end
			END
		IF (@operationType = 'Delete')
			BEGIN
				IF (@supervisor = 0)
						UPDATE dbo.EquipaFunc SET equipaId = NULL, supervisor = NULL WHERE funcId = @FuncId
				else 
					begin
						UPDATE dbo.EquipaFunc SET equipaId = nuLL, supervisor = NULL WHERE funcId = @FuncId
						UPDATE dbo.EquipaFunc SET equipaId = nuLL WHERE funcId = @supervisor 
						UPDATE dbo.EquipaFunc SET supervisor = NULL WHERE supervisor = @supervisor 
					end
			END
		COMMIT
	END
END


-- =============================================
-- Description:	i) Criar uma função para produzir a listagem (código, descrição) das intervencões de um determinado ano;
-- =============================================
go
CREATE OR ALTER FUNCTION dbo.IntervencaoAno (@year int)
RETURNS TABLE
AS
RETURN (SELECT i.id, i.descricao FROM dbo.Intervencao i
		WHERE YEAR(i.dtInicio) = @year AND YEAR(i.dtFim) = @year)

-- =============================================
-- Description:	j) Actualizar o estado de uma intervencao;
-- =============================================
GO
CREATE OR ALTER PROCEDURE DBO.SP_AtualizarEstadoIntervencao (@intervencaoID int, @novoEstado varchar(20) )
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED 
BEGIN
	if exists (select id froM Intervencao where id = @intervencaoID)
		begin 
			BEGIN TRANSACTION
			BEGIN TRY
				UPDATE Intervencao SET estado = @novoEstado WHERE id = @intervencaoID
			end try
			begin catch
				rollback transaction
			end catch
			COMMIT
		end
	ELSE
		RAISERROR ('Intervencao nao existe', 16, 1);
END
GO

-- =============================================
-- Description:	k) Criar uma vista que mostre o resumo das interven¸c˜oes (atributos de interven¸c˜ao e activo)
--, que possibilite a altera¸c˜ao do estado de uma ou mais interven¸c˜oes;;
-- ============================================

CREATE or alter VIEW dbo.TV_ResumoIntervencoes  
AS  (
SELECT	i.id AS IdIntervencao
		,i.descricao as descIntervencao
		,i.estado as estadoIntervencao
		,i.dtInicio as dtInIntervencao
		,i.dtFim as dtFimIntervencao
		,i.valor as valorIntervencao
		,a.nome as nomeAtivo
		,a.dtAquisicao as dtAquisicaoAtivo
		,a.marca as marcaAtivo
		,a.modelo as modeloAtivo
		,a.localizacao as locAtivo
		,a.valor as valorAtivo
FROM Intervencao i 
	INNER JOIN Ativos a on i.ativoId=a.id
)

GO
CREATE OR  ALTER TRIGGER [dbo].[trg_viewResumoInterv] 
ON dbo.TV_ResumoIntervencoes  
instead of update
as
BEGIN
	IF UPDATE (estadoIntervencao)
		BEGIN
			DECLARE @interven int,
					@estado varchar(20)
			declare c cursor for 
				select IdIntervencao , estadoIntervencao FROM inserted
			open c
			fetch next from c into @interven, @estado
			while @@FETCH_STATUS = 0
			BEGIN
						begin try
							UPDATE Intervencao SET estado = @estado Where id = @interven
						end try
						begin catch
							DECLARE @Exception varchar (5000)
							select @Exception = ERROR_MESSAGE()
							PRINT (@Exception)
						end catch
			fetch next from c into @interven, @estado
			end
			close c
			deallocate c
		END
	else
		RAISERROR ('Só é possivel alterar o estado da Intervencao', 16, 1);
END

