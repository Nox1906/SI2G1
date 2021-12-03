--(f) Criar o procedimento p criaInter que permite criar uma intervencao;

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Diogo Fernandes, Tiago Ribeiro
-- Create date: 29-11-2021
-- Description:	p_criaInter
-- =============================================
USE L51NG1
SET DATEFORMAT YMD
GO
CREATE OR ALTER PROCEDURE SP_criaInter
	@id int,
	@descricao varchar(50),
	@dtInicio date,
	@dtFim date,
	@valor money,
	@ativoId int,
	@meses int

AS	
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  --Apenas estou a ler uma vez
if ((SELECT dtAquisicao FROM DBO.Ativos WHERE id = @id) < @dtInicio)
	begin
		BEGIN TRANSACTION
		INSERT INTO dbo.Intervencao VALUES(@id,@descricao,'por atribuir',@dtInicio,@dtFim,@valor,@ativoId)
		INSERT INTO dbo.EquipaIntervencao VALUES (@id,null)
		if (@meses > 0)
			INSERT INTO dbo.IntervencaoPeriodica values (@id, @meses)
		COMMIT
	END
ELSE
	BEGIN
		RAISERROR('Data da Intervencao maior que a data de aquisicao, linha não foi inserida',16,1)
	END

GO
