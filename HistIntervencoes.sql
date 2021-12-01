USE [L51NG1]
GO
/****** Object:  Trigger [dbo].[trg_UpdateEquipaIntervencao]    Script Date: 29/11/2021 12:10:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create OR ALTER TRIGGER [dbo].[trg_UpdateEquipaIntervencao] 
ON [dbo].[EquipaIntervencao]
AFTER UPDATE 
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int,
			@Data datetime = GETDATE()

--não pode ter menos de dois funcionarios sendo um deles supervisor
IF EXISTS (	
			Select * 
			from [EquipaIntervencao] ei
				inner join inserted i on i.idIntervencao = ei.idIntervencao
				LEFT JOIN Equipa e on e.id = ei.equipaId
			where e.nFunc < 2 AND E.id not IN (SELECT equipaId from EquipaFunc where supervisor != 0)
			)
	BEGIN 
		RAISERROR ('Equipa tem de ter pelo menos 2 funcionarios sendo um deles o supervisor', 16, 1);
	END

--não pode ter mais de 3 intervenções atribuidas e apenas uma em curso

--a pessoa que gere o ativo nao pode participar na intervencao


--a descricao da intervencao tem de ser compativel com  as competencias 


END




GO

CREATE OR ALTER TRIGGER [dbo].[trg_DeleteEquipaIntervencao] 
ON [dbo].[EquipaIntervencao]
FOR DELETE
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int,
			@Data datetime = GETDATE()
			SELECT @Intervencao = idIntervencao , @Equipa = equipaId FROM deleted
		
		DELETE FROM dbo.HistAlteracaoEqInterv where @Intervencao = idIntervencao
END