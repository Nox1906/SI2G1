CREATE TABLE dbo.Competencias (
	id int NOT NULL ,
	descricao varchar(50) NOT NULL
	primary key (id)
);

CREATE TABLE dbo.Funcionarios (
	id int NOT NULL, 
	nome varchar(50) not null,
	cc int not null check (len(cc) = 8),
	nif int not null check (nif like '1________' or nif like '2________' ),
	dtNasc date not null, 
	endereco varchar (50) not null, 
	email varchar (50) check(email like ('%___@___%.__%')),
	ntelefone nvarchar(50) check(ntelefone like ('+351[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')), 
	profissao varchar(50),
	primary key (id),
	UNIQUE (email,cc,nif),

);

CREATE TABLE dbo.FunCompet (
	idFunc int not null,
	idComp int not null,
	PRIMARY KEY (idFunc, idComp),
	FOREIGN KEY (idFunc) REFERENCES Funcionarios(id),
	FOREIGN KEY (idFunc) REFERENCES Competencias(id),
);

CREATE TABLE dbo.Tipos (
	id int NOT NULL,
	descricao varchar(50) not null
	primary key (id)
);


CREATE TABLE dbo.Ativos (
	id int NOT NULL,
	nome varchar(50) not null,
	dtAquisicao date not null, 
	estado bit,
	marca varchar(50),
	modelo varchar(50),
	localizacao varchar(50) not null,
	parentId int,
	tipoId int NOT NULL,
	gestorId int not null,
	PRIMARY KEY (id),
	FOREIGN KEY (parentId) REFERENCES Ativos(id),
	FOREIGN KEY (tipoId) REFERENCES Tipos(id),
	FOREIGN KEY (gestorId) REFERENCES Funcionarios(id),
);

CREATE TABLE dbo.Historico (
	idAtivo int not null,
	dtAlteracao date not null,
	valor money,
	PRIMARY KEY (idAtivo,dtAlteracao),
	FOREIGN KEY (idAtivo) REFERENCES Ativos(id),
);

CREATE TABLE dbo.Equipa(
	id int not null,
	localizacao varchar(50),
	nFunc int,
	PRIMARY KEY (id),
);

CREATE TABLE dbo.EquipaFunc (
	funcId int not null,
	equipaId int not null, 
	supervisor int not null,
	PRIMARY KEY (funcId),
	FOREIGN KEY (funcId) REFERENCES Funcionarios(id),
	FOREIGN KEY (equipaId) REFERENCES Equipa(id),
	FOREIGN KEY (supervisor) REFERENCES Funcionarios(id),
);

CREATE TABLE dbo.Intervencao (
	id int not null , 
	descricao varchar (50) not null CHECK (descricao in ('avaria','rutura','analise')),
	estado varchar (50) not null CHECK (estado in('por atribuir','em analise','em execucao','concluido')),
	dtInicio date NOT NULL, 
	dtFim date,
	valor money,
	ativoId int not null,
	PRIMARY KEY (id),
	FOREIGN KEY (ativoId) REFERENCES Ativos(id),
	constraint finalh check( 
						dtFim > dtInicio 
						),
);

CREATE TABLE dbo.IntervencaoPeriodica (
	id int not null,
	meses int not null, 
	PRIMARY KEY (id),
	FOREIGN KEY (id) REFERENCES Intervencao(id),
);

CREATE TABLE dbo.EquipaIntervencao (
	idIntervencao int not null,
	equipaId int, 
	dataAlteracao date,
	PRIMARY KEY (idIntervencao,dataAlteracao),
	FOREIGN KEY (idIntervencao) REFERENCES Intervencao(id),
	FOREIGN KEY (equipaId) REFERENCES Equipa(id),
);
