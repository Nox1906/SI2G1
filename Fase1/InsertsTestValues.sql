USE L51NG1
go
SET XACT_ABORT ON

begin transaction
set dateformat dmy
INSERT INTO Funcionarios VALUES (1,'Diogo Fernandes','12846247','230111226','29-11-1985','Rua A', 'a@a.pt','962747088','Perito')
INSERT INTO Funcionarios VALUES (2,'Tiago Ribeito','12846241','230111225','29-11-1985','Rua b', 'B@B.pt','962747088','Engenheiro')
INSERT INTO Funcionarios VALUES (3,'Carlos Leandro','12846240','230111224','29-11-1985','Rua C', 'C@C.pt','962747088','Trolha')
INSERT INTO Funcionarios VALUES (4,'Joao Afonso','12846248','230111223','29-11-1985','Rua D', 'D@D.pt','962747088','Perito')
INSERT INTO Funcionarios VALUES (5,'Quim Barreiros','12846249','230111222','29-11-1985','Rua E', 'E@E.pt','962747088','Engenheiro')
INSERT INTO Funcionarios VALUES (6,'To Ze','12846243','230111221','29-11-1985','Rua G', 'G@G.pt','962747088','Trolha')
INSERT INTO Funcionarios VALUES (7,'Joao','12844243','230131221','30-11-1985','Rua G', 'G@G.pt','962747083','Trolha')
INSERT INTO Funcionarios VALUES (8,'Manel','22844243','235131221','02-11-1985','Rua Z', 'Z@G.pt','962747983','Trolha')

INSERT INTO Competencias VALUES (1,'rutura')
INSERT INTO Competencias VALUES (2,'avaria')
INSERT INTO Competencias VALUES (3,'inspecção')

INSERT INTO FunCompet VALUES (1,2)
INSERT INTO FunCompet VALUES (1,1)
INSERT INTO FunCompet VALUES (2,2)
INSERT INTO FunCompet VALUES (3,3)
INSERT INTO FunCompet VALUES (4,2)
INSERT INTO FunCompet VALUES (5,2)
INSERT INTO FunCompet VALUES (6,2)

INSERT INTO Tipos values (1, 'Eletrodomestico')
INSERT INTO Tipos values (2, 'Eletronica')

INSERT INTO Ativos values (1, 'Frigorifico', 60 , '29-10-2021' ,1 ,'Qualquer','Outro', 'Lisboa' ,1,1,6)
INSERT INTO Ativos values (2, 'Computador', 50 , '29-10-2021' ,1 ,'ACCER','ROG', 'Lisboa' ,1,2,5)
INSERT INTO Ativos values (3, 'MotherBoard', 50 , '29-10-2021' ,1 ,'ACCER','ROG', 'Lisboa',1,2,5)
INSERT INTO Ativos values (4,'Tubo refrigerador',10,'03-12-2021',1,'BOSS','2.0','Porto',1,1,6)


INSERT INTO Intervencao Values (1,'rutura','em análise','30-10-2021', '03-12-2021',50,1)
INSERT INTO Intervencao Values (2,'avaria','em análise','30-10-2021','01-11-2021',5,2)
INSERT INTO Intervencao Values (3,'avaria','em análise','01-10-2022','01-11-2022',5,3)


INSERT INTO Equipa VALUES (1, 'Lisboa',0)
INSERT INTO Equipa VALUES (2, 'Porto', 0)
INSERT INTO Equipa VALUES (3, 'Porto', 0)
INSERT INTO Equipa VALUES (4, 'Faro', 0)

COMMIT

SET XACT_ABORT OFF


