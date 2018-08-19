USE [ProjectManagr]
GO
SET IDENTITY_INSERT [dbo].[ProjectSites] ON 

INSERT [dbo].[ProjectSites] ([Id], [ProjectId], [EntityStatusId], [SiteId], [SiteItmFeedbackId], [DepartmentId], [ApplicationTypeId], [Apex], [PotentialValue], [SiteItm], [SiteEngagementStart], [SiteEngagementEnd], [HasBusinessImpact], [CommentsAndIssues], [IsResourceRequired], [Attachment], [IsActive], [ModifiedDate], [ModifiedBy], [CreatedDate], [CreatedBy]) VALUES (1, 1, 1, 5, 1, 1, 1, N'Test', CAST(12.00 AS Decimal(18, 2)), N'TEst', CAST(N'2018-08-01 00:00:00.000' AS DateTime), CAST(N'2018-08-31 00:00:00.000' AS DateTime), 1, N'Test', 1, N'Test', 1, CAST(N'2018-08-15 08:07:49.133' AS DateTime), N'', CAST(N'2018-08-15 07:12:31.000' AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[ProjectSites] OFF
