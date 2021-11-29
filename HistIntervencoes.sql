USE [L51NG1]
GO
/****** Object:  Trigger [dbo].[trg_UpdateEquipaIntervencao]    Script Date: 29/11/2021 12:10:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create OR ALTER TRIGGER [dbo].[trg_UpdateEquipaIntervencao] 
ON [dbo].[EquipaIntervencao]
FOR UPDATE 
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int,
			@Data datetime = GETDATE()
			SELECT @Intervencao = idIntervencao , @Equipa = equipaId FROM inserted

	INSERT INTO dbo.HistAlteracaoEqInterv values (@Intervencao, @Equipa,  @Data);
END

GO

CREATE OR ALTER TRIGGER [dbo].[trg_InsertEquipaIntervencao] 
ON [dbo].[EquipaIntervencao]
FOR INSERT
AS 
BEGIN
			DECLARE @Intervencao int, 
		    @Equipa int,
			@Data datetime = GETDATE()

			SELECT @Intervencao = idIntervencao , @Equipa = equipaId FROM inserted
			
		INSERT INTO dbo.HistAlteracaoEqInterv values (@Intervencao, @Equipa,  @Data);
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