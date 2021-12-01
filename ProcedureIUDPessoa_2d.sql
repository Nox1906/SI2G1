USE L51NG1
CREATE PROCEDURE Funcionarioinsertupdatedelete (@id int,
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
	    DELETE FROM Funcionarios
	    WHERE  id = @id
	END
END

drop PROCEDURE Funcionarioinsertupdatedelete