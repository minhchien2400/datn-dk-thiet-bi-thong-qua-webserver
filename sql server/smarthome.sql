use SmartHome
go

create table remote(
Id int primary key,
KeyRemote int,
)

insert into firealarm(Id,KeyFire) values(1,1)
insert into temphum(Id,Temp,Hum) values(1,20,30)
insert into remote(Id,KeyRemote) values(1,0)
insert into statusbulb(Id,Status,Color) values(1,0,1)
insert into statusbulb(Id,Status,Color) values(2,0,1)
insert into statusair(Id,Status,Mode,Speed,Temp) values(1,0,1,1,25)
insert into statusspeak(Id,Status,PlayPause,Volume) values(1,0,0,1)

select * from remote
SET IDENTITY_INSERT statusbulb Off
SET IDENTITY_INSERT temphum Off
SET IDENTITY_INSERT remote Off
SET IDENTITY_INSERT firealarm Off
SET IDENTITY_INSERT statusair On
SET IDENTITY_INSERT statusspeak Off