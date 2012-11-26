drop table dbo.Followers
go

create table dbo.Followers (
	UserId int not null,
	FollowerUserId int not null,
	constraint Followers_pk primary key (UserId,FollowerUserId)
)
go

create index followers_ix1 on Followers (FollowerUserId)
go

