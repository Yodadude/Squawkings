create table Users (
	UserId serial primary key,
    UserName   varchar(50),
	FirstName  varchar(50),
	LastName   varchar(50),
	Email      varchar(300),
    AvatarUrl  varchar(300),
	bio        varchar(500),
	IsGravatar smallint not null default 0
)
;

create table UserSecurityInfo (
	UserId int primary key,
	Password varchar(100),
	foreign key (UserId) references Users(UserId)
)
;

create table Squawks (
    SquawkId  serial primary key,
    UserId    int not null,
    CreatedAt timestamptz not null,
    Content   varchar(200)
)
;
create table Followers (
	UserId         int not null,
	FollowerUserId int not null,
	constraint Followers_pk primary key (UserId,FollowerUserId)
)
;

create index followers_ix1 on Followers (UserId)
;

create index followers_ix2 on Followers (FollowerUserId)
;

