create database luby_tasks  collate Latin1_General_CS_AS;
go

use luby_tasks;
go

create table dbo.users
(
	id int identity(1,1) primary key not null,
	[name] nvarchar(150) collate Latin1_General_CS_AS not null,
	[login] nvarchar(50) collate Latin1_General_CS_AS not null,
	[password] nvarchar(512) collate Latin1_General_CS_AS not null,
	create_date datetimeoffset not null default getdate(),
	last_modified datetimeoffset not null default getdate(),
	removed bit not null default 0
);
go
create unique index U_Login on dbo.users (login) where removed = 0
go

create table tasks
(
	id int identity(1,1) primary key not null,
	[title] nvarchar(150) collate Latin1_General_CS_AS not null,
	[description] nvarchar(4000) collate Latin1_General_CS_AS,
	concluded bit not null default 1,
	[id_user] int not null,
	create_date datetimeoffset not null default getdate(),
	last_modified datetimeoffset not null default getdate(),
	removed bit not null default 0
		constraint FK_users_1 foreign key ([id_user]) references dbo.users
);
go