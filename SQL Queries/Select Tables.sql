/****** Script for SelectTopNRows command from SSMS  ******/
select * from sites --1 to 31
select * from Countries -- 1 to 21
select * from SiteItmFeedbacks -- 1 to 3
select * from Departments -- 1
select * from ApplicationTypes -- 1 to 3
select * from EntityStatus -- 1 to 8
select * from SubPortfolios -- 1 to 11
select * from Projects
select * from ProjectSites where IsActive = 1
select * from Managers
--update Projects set PmName='Ranjith Vijayabaskar'
--update ProjectSites set SiteItm='Ranjith Vijayabaskar'
--delete from ProjectSites
--where id in (
--select top 1934 id from ProjectSites)

--delete ProjectSites where Name ='This is a very very very very big Project Name'

DBCC CHECKIDENT ('Projects', RESEED, 0);
GO

--update Managers
--set Name = 'Spoorthi Muniappa'
--where Id = 3