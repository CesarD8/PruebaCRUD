Create Database test1

Create table Food(
ID int IDENTITY,
Name varchar(255),
Price money,
Style varchar(255),
PRIMARY KEY (ID)
)

Create table FoodBackup(
ID int,
Name varchar(255),
Price money,
Style varchar(255),
PRIMARY KEY (ID)
)

drop table FoodBackup
drop table Food

Create trigger OnInsert
on Food
After Insert
as
begin
Insert into FoodBackup(ID, Name, Price, Style)
select ID,Name,Price,Style from inserted
end
go

Create trigger OnDelete
on Food
for Delete
as
begin
Delete b from FoodBackup b Join deleted as a ON b.ID = a.ID
end
go


drop trigger OnDelete
drop trigger OnInsert

insert into Food values('Taco', 15, 'Mexican')
insert into Food values('Beef Taco', 25, 'Mexican')
insert into Food values('Lemonade', 12, 'Universal')

select * from Food
select * from FoodBackup

delete from food where Name = 'Beef Taco'

update food set Name='Hamburger', Price=14 where Name = 'Hamburger'

Select * from food where Name like 'Ta%'

select * from food as f join foodBackup as a ON f.ID = a.ID

Delete b from FoodBackup b Join Food as a ON b.ID = a.ID