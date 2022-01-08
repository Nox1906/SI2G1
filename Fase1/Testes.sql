-- =============================================
-- Description:	TESTES d ) Criar o procedimento para criaInter que permite criar uma intervencao;
-- =============================================
BEGIN 
	PRINT('TESTES ALINEA D - INSERT')
	BEGIN TRAN
		EXEC DBO.SP_Funcionarios 8,'Toni Mota','14599943','240131221','01-11-1990','Rua z', 'z@z.pt','962247083','Engenheiro','Insert'
		DECLARE @userInserido int
		select @userInserido = count(id) from dbo.Funcionarios WHERE ID = 8
		if (@userInserido = 1)
			PRINT ('USER INSERIDO OK')
		ELSE 
			PRINT ('USER INSERIDO NOK')
	ROLLBACK
end
USE L51NG1

GO
BEGIN 
	PRINT('TESTES ALINEA D - update')
	BEGIN TRAN
		EXEC DBO.SP_Funcionarios 7,'Maria','14599943','240131221','01-11-1990','Rua z', 'z@z.pt','962247083','Engenheiro','Update'
		DECLARE @userAtualizado int
		select @userAtualizado = count(id) from dbo.Funcionarios WHERE nome = 'Maria'
		if (@userAtualizado = 1)
			PRINT ('USER ATUALIZADO OK')
		ELSE 
			PRINT ('USER ATUALIZADO NOK')
	ROLLBACK
end
go
BEGIN 
	PRINT('TESTES ALINEA D - DELETE')
	BEGIN TRAN
		EXEC DBO.SP_Funcionarios 7,'','','','','', '','','','Delete'
		DECLARE @userEliminado int
		select @userEliminado = count(id) from dbo.Funcionarios WHERE id = 7
		if (@userEliminado = 0)
			PRINT ('USER ELIMINADO OK')
		ELSE 
			PRINT ('USER ELIMINADO NOK')
	ROLLBACK
end

-- =============================================
-- Description:	TESTES e ) Obter o cOodigo de uma equipa livre, dada uma descri¸c˜ao de interven¸c˜ao, capaz de
--resolver o problema. Em caso de haver v´arias equipas deve escolher-se a que teve uma
--intervenãao atribuida á mais tempo;
-- =============================================
go
BEGIN 
	PRINT('TESTES ALINEA E - Obter equipa livre')
	BEGIN TRAN
				EXEC dbo.SP_ActualizarElementosEquipa 1,2,'insert', 3
				EXEC dbo.SP_ActualizarElementosEquipa 2,1,'insert', 4
				EXEC dbo.SP_ActualizarElementosEquipa 3,5,'insert', 6
				UPDATE EquipaIntervencao SET equipaId = 1 WHERE idIntervencao = 2
				UPDATE EquipaIntervencao SET equipaId = 2 WHERE idIntervencao = 1
		DECLARE @equipaLivre int
		SELECT @equipaLivre = dbo.F_ObterEquipaLivre('avaria')
		if (@equipaLivre = 1)
			PRINT ('Equipa 1 está livre OK')
		ELSE 
			PRINT ('Equipa 1 está livre NOK')
	ROLLBACK
end
go
BEGIN 
	PRINT('TESTES ALINEA E - Obter equipa livre')
	BEGIN TRAN
				EXEC dbo.SP_ActualizarElementosEquipa 1,2,'insert', 3
				EXEC dbo.SP_ActualizarElementosEquipa 2,1,'insert', 4
				EXEC dbo.SP_ActualizarElementosEquipa 3,5,'insert', 6
				UPDATE EquipaIntervencao SET equipaId = 1 WHERE idIntervencao = 2
				UPDATE EquipaIntervencao SET equipaId = 2 WHERE idIntervencao = 1
		DECLARE @equipaLivre int
		SELECT @equipaLivre = dbo.F_ObterEquipaLivre('inspeção')
		if (@equipaLivre IS NULL)
			PRINT ('Equipa nao encontrada OK')
		ELSE 
			PRINT ('Equipa nao encontrada NOK')
	ROLLBACK
end

-- =============================================
-- Description:	TESTES f ) Criar o procedimento p criaInter que permite criar uma interven¸c˜ao;
-- =============================================

GO
set dateformat dmy
BEGIN 
	PRINT('TESTES ALINEA F - CRIAR INTERVENCAO')
	BEGIN TRAN
		EXEC DBO.SP_criaInter 4,'avaria', '01-12-2021', '02-12-2021',50,1,0
		DECLARE @Intervencao int
		select @Intervencao = count(id) from dbo.Intervencao WHERE ID = 4
		if (@Intervencao = 1)
			PRINT ('INTERVENCAO CRIADA OK')
		ELSE 
			PRINT ('INTERVENCAO CRIADA NOK')
	ROLLBACK
end

go
BEGIN 
	PRINT('TESTES ALINEA F - CRIAR INTERVENCAO COM DATA DE INICIO INFERIOR A DATA DE AQUISICAO')
	BEGIN TRAN
		EXEC DBO.SP_criaInter 4,'avaria', '28-10-2021', '02-12-2021',50,1,0
		DECLARE @IntervencaoDT int
		select @IntervencaoDT = count(id) from dbo.Intervencao WHERE ID = 4
		if (@IntervencaoDT = 0)
			PRINT ('INTERVENCAO NAO FOI CRIADA OK')
		ELSE 
			PRINT ('INTERVENCAO FOI CRIADA NOK')
	ROLLBACK
end

-- =============================================
-- Description:	TESTES G ) Implementar o mecanismo que permite criar uma equipa;
-- =============================================

GO
BEGIN 
	PRINT('TESTES ALINEA G - CRIAR INTERVENCAO COM DATA INFERIOR')
	BEGIN TRAN
		EXEC [dbo].[SP_criaEquipa] 5, 'Matosinhos'
		DECLARE @countEquipa int
		select @countEquipa = count(id) from dbo.Equipa WHERE ID = 5
		if (@countEquipa = 1)
			PRINT ('EQUIPA CRIADA OK')
		ELSE 
			PRINT ('EQUIPA CRIADA NOK')
	ROLLBACK
end

-- =============================================
-- Description:	TESTES h ) Actualizar (adicionar ou remover) os elementos de uma equipe e associar as respectivas competências;
-- =============================================

GO
BEGIN 
	PRINT('TESTES ALINEA h - adicionar elemento à equipa')
	BEGIN TRAN
		EXEC [dbo].[SP_ActualizarElementosEquipa] 4, 1, 'Insert', 2
		DECLARE @elem int,
				@nFunc int
		select @elem = count(equipaId) from dbo.EquipaFunc WHERE equipaId = 4
		select @nFunc = nFunc from dbo.Equipa WHERE id = 4
		if (@elem = 2 AND @nFunc = 2)
			PRINT ('Elemento inserido e atualizdo nFunc de equipa OK')
		ELSE 
			PRINT ('Elemento inserido e atualizdo nFunc de equipa NOK')
		ROLLBACK
end

GO
BEGIN 
	PRINT('TESTES ALINEA h - remover elemento à equipa')
	BEGIN TRAN
		EXEC [dbo].[SP_ActualizarElementosEquipa] 4, 1, 'Delete', 2
		EXEC [dbo].[SP_ActualizarElementosEquipa] 4, 2, 'Delete', 2
				DECLARE @elemEi int,
				@nFuncE int
		select @elemEi = count(equipaId) from dbo.EquipaFunc WHERE equipaId = 4
		select @nFuncE = nFunc from dbo.Equipa WHERE id = 4
		if (@elemEi = 0 AND @nFuncE = 0)
			PRINT ('Elemento removido e atualizdo nFunc de equipa OK')
		ELSE 
			PRINT ('Elemento inserido e atualizdo nFunc de equipa NOK')
	ROLLBACK
end

-- =============================================
-- Description:	TESTES I ) Criar uma funcão para produzir a listagem (código, descriçao) das intervencoes de um determinado ano;
-- =============================================

GO
BEGIN 
	PRINT('TESTES ALINEA I - Obter intervencoes num ano')
		if ((SELECT COUNT(1) from dbo.IntervencaoAno(2021)) = 2)
			PRINT ('numero de intervencoes OK')
		ELSE 
			PRINT ('numero de intervencoes NOK')
end

-- =============================================
-- Description:	TESTES J ) Actualizar o estado de uma interven¸c˜ao;
-- =============================================

GO
BEGIN 
	PRINT('TESTES ALINEA J - Atualizar estado ')
	BEGIN TRAN
		EXEC [dbo].[SP_ActualizarElementosEquipa] 1, 1, 'Insert', 2
		update EquipaIntervencao set equipaId = 1 where idIntervencao = 1
		EXEC dbo.SP_AtualizarEstadoIntervencao 2, 'em execução'
		DECLARE @Interv int
		select @Interv = count(estado) from dbo.Intervencao WHERE estado = 'em execução'
		if (@Interv = 1)
			PRINT ('ESTADO ATUALIZADO OK')
		ELSE 
			PRINT ('ESTADO ATUALIZADO NOK')
	ROLLBACK
end

GO
BEGIN 
	PRINT('TESTES ALINEA J - Atribuir duas execuções à mesma equipa')
	begin try
		BEGIN TRAN
			EXEC [dbo].[SP_ActualizarElementosEquipa] 1, 1, 'Insert', 2
			update EquipaIntervencao set equipaId = 1 where idIntervencao = 1
			update EquipaIntervencao set equipaId = 1 where idIntervencao = 2
			--apos update atualiza o estado para em 'em análise nas intervencoes'
			EXEC dbo.SP_AtualizarEstadoIntervencao 1, 'em execução'
			EXEC dbo.SP_AtualizarEstadoIntervencao 2, 'em execução'
				print ('atualizados dois estados de duas intervenções da mesma equipa para em Execução - NOK')
		ROLLBACK
	END TRY
	BEGIN CATCH
		PRINT ('Equipa já tem uma intervenção em execução, linhas nao foram inseridas - OK')
	END CATCH
end

-- =============================================
-- Description:	TESTES K ) Criar uma vista que mostre o resumo das interven¸c˜oes (atributos de interven¸c˜ao e activo)
--, que possibilite a altera¸c˜ao do estado de uma ou mais interven¸c˜oes;;
-- =============================================

GO
BEGIN 
	PRINT('TESTES ALINEA K - atualizar varios estados')
		BEGIN TRAN
			UPDATE TV_ResumoIntervencoes SET estadoIntervencao = 'em execução'
			DECLARE @Interv int
			select @Interv = count(estado) from dbo.Intervencao WHERE estado = 'em execução'
			if (@Interv = 3)
				print ('atualizados 3 intervencoes para em Execução - Ok')
			else
				print ('atualizados 3 intervencoes para em Execução - NOK')
		ROLLBACK	
end

GO
BEGIN 
	PRINT('TESTES ALINEA K - atualizar varios estados')
		BEGIN TRAN
			UPDATE TV_ResumoIntervencoes SET descIntervencao = 'rutura'
			DECLARE @Interv int
			select @Interv = count(descricao) from dbo.Intervencao WHERE descricao = 'rutura'
			if (@Interv = 3)
				print ('falha a atualiza um campo diferente de estado - NOK')
			else
				print ('falha a atualiza um campo diferente de estado - OK')
		ROLLBACK	
end