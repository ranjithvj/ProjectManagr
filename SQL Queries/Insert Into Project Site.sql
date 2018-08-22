declare @id int 
select @id = 1
while @id >=1 and @id <= 1000
begin

INSERT INTO [dbo].[ProjectSites]
           ([ProjectId]
           ,[EntityStatusId]
           ,[SiteId]
           ,[SiteItmFeedbackId]
           ,[DepartmentId]
           ,[ApplicationTypeId]
           ,[Apex]
           ,[PotentialValue]
           ,[SiteItm]
           ,[SiteEngagementStart]
           ,[SiteEngagementEnd]
           ,[HasBusinessImpact]
           ,[CommentsAndIssues]
           ,[IsResourceRequired]
           ,[Attachment]
           ,[IsActive]
           ,[ModifiedDate]
           ,[ModifiedBy]
           ,[CreatedDate]
           ,[CreatedBy])
     VALUES
           (@id%2 +1
           ,@id%8 + 1
           ,@id % 34 + 1
           ,@id%3 + 1
           ,1
           ,@id%2 + 1
           ,'Test Apex'
           ,@id * 3
           ,'Ranjith.Vijayabaskar'
           ,DATEADD(day,(0 - ABS(CHECKSUM(NewId())) % 365),getdate())
           ,DATEADD(day,ABS(CHECKSUM(NewId())) % 365,getdate())
           ,1
           ,'Test'
           ,1
           ,'Test'
           ,1
           ,GETDATE()
           ,'Ranjith.Vijayabaskar'
           ,GETDATE()
           ,'Ranjith.Vijayabaskar')

		    select @id = @id + 1
end



