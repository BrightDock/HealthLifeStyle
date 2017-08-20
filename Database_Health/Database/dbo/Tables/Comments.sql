CREATE TABLE [dbo].[Comments]
(
	[ID] BIGINT NOT NULL IDENTITY (1, 1), 
    [Author_ID] BIGINT NOT NULL, 
    [Text] NVARCHAR(MAX) NOT NULL, 
    [Target_type] NCHAR(20) NOT NULL, 
    [Target_ID] BIGINT NOT NULL, 
    [Date] DATETIME NOT NULL,
    [Target_user] BIGINT NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Comments_Users] FOREIGN KEY (Author_ID) REFERENCES [dbo].[Users] ([ID])
)
