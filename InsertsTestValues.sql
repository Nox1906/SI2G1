SET XACT_ABORT ON
begin transaction
set dateformat dmy
INSERT INTO Funcionarios VALUES (1,'Diogo Fernandes','12846247','230111222','29-11-1985','Rua A', 'a@a.pt','962747088','Pintor')
INSERT INTO Funcionarios VALUES (2,'Tiago Ribeito','12846247','230111222','29-11-1985','Rua A', 'B@B.pt','962747088','Escultor')
INSERT INTO Funcionarios VALUES (3,'Carlos Leandro','12846247','230111222','29-11-1985','Rua A', 'C@a.pt','962747088','Engenheiro')

INSERT INTO Tipos values (1, 'Escultura')
INSERT INTO Tipos values (2, 'Eletronica')

INSERT INTO Ativos values (1, 'Estatua', 60 , '29-10-2021' ,1 ,'Qualquer','Outro', 'Lisboa' ,1,1,3)
INSERT INTO Ativos values (2, 'Computador', 50 , '29-10-2021' ,1 ,'ACCER','ROG', 'Lisboa' ,1,1,2)

EXEC dbo.SP_criaInter 1,'rutura','01-01-2021', '03-12-2021',50,1
EXEC dbo.SP_criaInter 2,'avaria','30-10-2021','01-11-2021',5,2

INSERT INTO Equipa VALUES (1, 'Lisboa', 0)
INSERT INTO Equipa VALUES (2, 'Porto', 0)

INSERT INTO EquipaIntervencao values (1,null)

update EquipaIntervencao set equipaId = 2 where idIntervencao = 1

DELETE FROM Intervencao

DELETE FROM EquipaIntervencao
DELETE FROM HistAlteracaoEqInterv

commit

SET XACT_ABORT OFF

