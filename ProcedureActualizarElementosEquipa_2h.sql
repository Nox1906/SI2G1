use L51NG1
CREATE PROCEDURE ActualizarElementosEquipa (@equipaId int, @FuncId int, @operationType varchar(20))
AS
BEGIN
	DECLARE @nrFunc int
	SELECT @nrFunc = e.nFunc FROM dbo.Equipa e /* numero de funcionarios na equipa*/
	WHERE e.id = @equipaId

	IF @operationType = 'adicionar'
	BEGIN
		IF (SELECT count(ef.funcId) from dbo.EquipaFunc ef WHERE ef.funcId = @FuncId) = 0 /* verifica se funcionario nao existe noutra equipa*/
		BEGIN
			DECLARE @supervisor int
			SELECT DISTINCT @supervisor = ef.supervisor FROM dbo.EquipaFunc ef /* supervisor*/
			WHERE equipaId = @equipaId
			INSERT INTO dbo.EquipaFunc VALUES (@equipaId, @FuncId, @supervisor)
			UPDATE dbo.Equipa SET nFunc = @nrFunc+1 WHERE id = @equipaId
		END
		ELSE 
		BEGIN 
			RAISERROR('Funcionario ja faz parte de outra equipa',16,1)
		END
	END
	IF @operationType = 'remover'
	BEGIN
		DELETE FROM dbo.EquipaFunc 
	    WHERE  funcId = @FuncId
	    UPDATE dbo.Equipa SET nFunc = @nrFunc-1 WHERE id = @equipaId
	END
END

	
drop PROCEDURE ActualizarElementosEquipa


exec ActualizarElementosEquipa @equipaId = 2, @FuncId =4, @operationType = 'remover'





