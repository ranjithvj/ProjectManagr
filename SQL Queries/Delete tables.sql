DELETE EntityStatus
DELETE Departments
DELETE Countries
DELETE ApplicationTypes
DELETE SiteItmFeedbacks
DELETE Projects
DELETE Sites
delete SubPortfolios

DBCC CHECKIDENT ('dbo.EntityStatus', RESEED, 0);  
DBCC CHECKIDENT ('dbo.Departments', RESEED, 0);  
DBCC CHECKIDENT ('dbo.Countries', RESEED, 0);  
DBCC CHECKIDENT ('dbo.ApplicationTypes', RESEED, 0);  
DBCC CHECKIDENT ('dbo.SiteItmFeedbacks', RESEED, 0);  
DBCC CHECKIDENT ('dbo.Projects', RESEED, 0);  
DBCC CHECKIDENT ('dbo.Sites', RESEED, 0);
DBCC CHECKIDENT ('dbo.SubPortfolios', RESEED, 0);
