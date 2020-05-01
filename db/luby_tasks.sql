create database luby_tasks  collate Latin1_General_CS_AS;
go

use luby_tasks;
go

create table dbo.users (
	id int identity(1,1) primary key not null,
	name nvarchar(150) collate Latin1_General_CS_AS not null,
	login nvarchar(512) collate Latin1_General_CS_AS not null unique,
	password nvarchar(512) collate Latin1_General_CS_AS not null,
	create_date datetimeoffset not null default getdate(),
	last_modified datetimeoffset not null default getdate(),
	removed bit not null default 0
);
go

create table tasks (
	id int identity(1,1) primary key not null,
	description nvarchar(4000) collate Latin1_General_CS_AS not null,
	concluded bit not null default 1,
	[user_id] int not null,
	create_date datetimeoffset not null default getdate(),
	last_modified datetimeoffset not null default getdate(),
	removed bit not null default 0
	constraint FK_users_1 foreign key ([user_id]) references dbo.users
);
go