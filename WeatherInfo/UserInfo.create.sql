use WeatherInfo
-- drop TABLE dbo.UserInfo
go
/****** Object:  Table WeatherInfo.dbo].UserInfo    Script Date: 12/21/2014 6:46:24 PM ******/
set ansi_nulls on
go
set quoted_identifier on
go
create table dbo.UserInfo (
	UserId uniqueidentifier not null,
	FirstName nvarchar(50) not null, 
	LastName nvarchar(50) not null, 
	CreatedDate smalldatetime not null,
	IsActive bit not null constraint df_WeatherInfo_IsActive default 1,
 constraint pk_WeatherInfo primary key clustered 
(
	UserId ASC
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]
) on [primary]

insert into dbo.UserInfo (UserId, FirstName, LastName, CreatedDate, IsActive)
values (newid(), 'Default', 'User', getdate(), 1)

go

