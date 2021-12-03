USE L51NG1

GO
/****** Object:  Trigger [dbo].[trg_UpdateEquipaIntervencao]    Script Date: 29/11/2021 12:10:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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