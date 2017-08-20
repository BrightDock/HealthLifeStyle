CREATE TABLE [dbo].[WEB_Pages] (
    [ID]          BIGINT      IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (100) NOT NULL,
    [Auther_id]   BIGINT      NOT NULL,
    [Date]        DATETIME    NOT NULL,
    [Count_watch] BIGINT      DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WEB_Pages_Users] FOREIGN KEY ([Auther_id]) REFERENCES [dbo].[Users] ([ID]) ON DELETE CASCADE
);




