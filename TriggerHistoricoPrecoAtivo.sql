USE L51NG1
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