CREATE TABLE [dbo].[Likes]
(
	[ID] BIGINT NOT NULL IDENTITY (1, 1), 
    [Target_type] NCHAR(20) NOT NULL, 
    [Target_ID] BIGINT NOT NULL,
    [User_ID] BIGINT NOT NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Likes_Users] FOREIGN KEY (User_ID) REFERENCES [dbo].[Users] ([ID])
)
