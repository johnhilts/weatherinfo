use WeatherInfo
-- drop TABLE dbo.UserLocations
go
/****** Object:  Table dbo.UserLocations    Script Date: 12/21/2014 6:46:24 PM ******/
set ansi_nulls on
go
set quoted_identifier on
go
create table dbo.UserLocations(
	UserId uniqueidentifier not null,
	GeoLocation geography not null,
	City nvarchar(50) not null, 
	StateCode varchar(2) not null, 
	CountryCode varchar(2) not null, 
	SortOrder tinyint not null, 
 constraint [pk_UserLocations] primary key clustered 
(
	UserId asc
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]
) on [primary]
go

alter table dbo.UserLocations  with check add  constraint FK_UserLocations_UserInfo foreign key(UserId)
references dbo.UserInfo (UserId)
go
